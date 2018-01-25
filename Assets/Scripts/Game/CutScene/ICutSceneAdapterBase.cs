/*
 * LICENCE
 */
using UnityEngine;

namespace Game
{
	public interface ICutSceneAdapterBase
	{
		void SetText( string _text, bool _forChoice );
		void SetSprite( Sprite _sprite, int _position );
		void SetChoices( string[] _choices );
		bool ReceiveChoice( out int _choice );
		bool ReceiveOk();
		void StartCutScene( string _name );
		void EndCutScene();
		void StartSnapshot( int _index );
		void EndSnapshot();
	}
}
