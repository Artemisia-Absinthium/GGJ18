/*
 * MIT License
 *
 * Copyright (c) 2017 Joseph Kieffer
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */
#include <cstdio>
#include <cstdlib>
#include <vector>
#include <string>
#include <random>
#include <chrono>

 // Change the following values according to your game
#define GAME_FOLDER "Editor"
#define GAME_NAME "Unity.exe"

#ifdef _WIN32
#include <Windows.h>
#define PATH_SEPARATOR "\\"
#else
#define PATH_SEPARATOR "/"
#ifndef __argc
#define __argc _argc
#endif
#ifndef __argv
#define __argv _argv
#endif
#endif
#define HEADED_PATH PATH_SEPARATOR GAME_FOLDER PATH_SEPARATOR GAME_NAME
#define NO_HEAD_PATH "." HEADED_PATH

union UFast64ByteAccess
{
	unsigned long long int Int;
	unsigned char Bytes[ 8 ];
};

void ParseArgs( int _argc, char** _argv, std::vector<std::string>& _args );
void AddCustomArgs( std::vector<std::string>& _args );
void StartGameProcess( const std::vector<std::string>& _args );

#ifdef _WIN32
int WINAPI WinMain( HINSTANCE hInstance, HINSTANCE hPrevInstance, LPSTR lpCmdLine, int cmdShow )
#else
int main( int _argc, char** _argv )
#endif
{
	std::vector<std::string> args;
	ParseArgs( __argc, __argv, args );
	AddCustomArgs( args );

	StartGameProcess( args );

	return 0;
}

void ParseArgs( int _argc, char** _argv, std::vector<std::string>& _args )
{
	std::string exe = _argv[ 0 ];
	size_t lastSlashPosition = exe.find_last_of( "/\\" );
	if ( lastSlashPosition == std::string::npos )
	{
		_args.push_back( std::string( NO_HEAD_PATH ) );
	}
	else
	{
		std::string head = exe.substr( 0, lastSlashPosition );
		_args.push_back( head + std::string( HEADED_PATH ) );
	}

	for ( int i = 1; i < _argc; ++i )
	{
		_args.push_back( std::string( _argv[ i ] ) );
	}
}

void AddKeyArg( std::vector<std::string>& _args )
{
	unsigned long seed = std::chrono::system_clock::now().time_since_epoch().count();
	std::mt19937_64 generator( seed );
	UFast64ByteAccess access1, access2;
	access1.Int = generator();
	access2.Int = generator();

	static const unsigned char s_engineKey[ 32 ] =
	{
		0x02, 0x03, 0x19, 0x94,
		0x06, 0x10, 0x19, 0x95,
		0x04, 0x11, 0x20, 0x17,

		0x01, 0x06, 0x19, 0x50,
		0x01, 0x10, 0x19, 0x69,

		0x26, 0x11, 0x19, 0x85,
		0x20, 0x09, 0x19, 0x92,
		0x19, 0x08, 0x19, 0x95
	};

	unsigned char key[ 32 ];
	for ( int i = 0; i < 8; ++i )
	{
		key[ i ] = s_engineKey[ i ] ^ access1.Bytes[ i ];
		key[ i + 16 ] = s_engineKey[ 31 - i ] ^ access1.Bytes[ i ];
		key[ i + 8 ] = s_engineKey[ i + 8 ] ^ access2.Bytes[ i ];
		key[ i + 24 ] = s_engineKey[ 31 - i - 8 ] ^ access2.Bytes[ i ];
	}
	char keyString[ 70 ];
	keyString[ 0 ] = '+';
	keyString[ 1 ] = 'k';
	keyString[ 2 ] = 'e';
	keyString[ 3 ] = 'y';
	keyString[ 4 ] = '=';
	keyString[ 69 ] = 0;
	for ( int i = 0; i < 32; ++i )
	{
		if ( key[ i ] < 0xA0 )
		{
			keyString[ 5 + i * 2 ] = '0' + ( key[ i ] >> 4 );
		}
		else
		{
			keyString[ 5 + i * 2 ] = 'a' - 0xA + ( key[ i ] >> 4 );
		}
		if ( ( key[ i ] & 0xF ) < 0xA )
		{
			keyString[ 6 + i * 2 ] = '0' + ( key[ i ] & 0xF );
		}
		else
		{
			keyString[ 6 + i * 2 ] = 'a' - 0xA + ( key[ i ] & 0xF );
		}
	}
	_args.push_back( std::string( keyString ) );
}

void AddPathsArgs( std::vector<std::string>& _args )
{
	size_t lastSlashPosition = _args[ 0 ].find_last_of( "/\\" );
	if ( lastSlashPosition == std::string::npos )
	{
		return;
	}
	std::string gpath = _args[ 0 ].substr( 0, lastSlashPosition );
	std::string separator = std::string( PATH_SEPARATOR );
	lastSlashPosition = gpath.find_last_of( "/\\" );
	std::string lpath = gpath.substr( 0, lastSlashPosition );
	gpath += separator;
	lpath += separator;
	_args.push_back( std::string( "+gpath=" ) + gpath );
	_args.push_back( std::string( "+lpath=" ) + lpath );
}

void AddCustomArgs( std::vector<std::string>& _args )
{
	AddKeyArg( _args );
	AddPathsArgs( _args );
}

const wchar_t *GetWC( const char *c )
{
	const size_t cSize = strlen( c ) + 1;
	wchar_t* wc = new wchar_t[ cSize ];
	mbstowcs( wc, c, cSize );

	return wc;
}

void AppendArgument( const std::wstring& _argument, std::wstring& _commandLine )
{
	_commandLine.push_back( L'"' );

	for ( std::wstring::const_iterator it = _argument.begin(); ; ++it )
	{
		unsigned int numberBackslashes = 0;

		while ( it != _argument.end() && *it == L'\\' )
		{
			++it;
			++numberBackslashes;
		}

		if ( it == _argument.end() )
		{
			_commandLine.append( numberBackslashes * 2, L'\\' );
			break;
		}
		else if ( *it == L'"' )
		{
			_commandLine.append( numberBackslashes * 2 + 1, L'\\' );
			_commandLine.push_back( *it );
		}
		else
		{
			_commandLine.append( numberBackslashes, L'\\' );
			_commandLine.push_back( *it );
		}
	}
	_commandLine.push_back( L'"' );
	_commandLine.push_back( L' ' );
}

void StartGameProcess( const std::vector<std::string>& _args )
{
#ifdef _WIN32
	STARTUPINFO si = { sizeof( STARTUPINFO ) };
	si.cb = sizeof( si );
	si.dwFlags = STARTF_USESHOWWINDOW;
	si.wShowWindow = SW_SHOW;
	PROCESS_INFORMATION pi;

	std::wstring commandLine;

	for ( int i = 0; i < _args.size(); ++i )
	{
		const wchar_t * arg = GetWC( _args[ i ].c_str() );
		AppendArgument( std::wstring( arg ), commandLine );
		delete[] arg;
	}

	wchar_t * commandLineBuffer = new wchar_t[ commandLine.size() + 1 ];
	commandLineBuffer[ commandLine.size() ] = 0;
	memcpy( commandLineBuffer, commandLine.c_str(), commandLine.size() * sizeof( wchar_t ) );

	if ( !CreateProcess(
		NULL,
		commandLineBuffer,
		NULL,
		NULL,
		FALSE,
		CREATE_NO_WINDOW,
		NULL,
		NULL,
		&si,
		&pi ) )
	{
	delete[] commandLineBuffer;
		MessageBox( NULL, L"Unable to start the game", L"Error starting process", MB_ICONERROR | MB_OK );
		exit( 1 );
	}

	delete[] commandLineBuffer;

	WaitForSingleObject( pi.hProcess, INFINITE );

	CloseHandle( pi.hProcess );
	CloseHandle( pi.hThread );

#else
#endif
}