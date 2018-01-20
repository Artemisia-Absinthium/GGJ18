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
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Engine
{
	[System.Serializable]
	public class InputXMLParser
	{
		[XmlRoot( ElementName = "axis" )]
		public class Axis
		{
			[XmlAttribute( AttributeName = "name" )]
			public string Name { get; set; }
			[XmlAttribute( AttributeName = "positive" )]
			public string Positive { get; set; }
			[XmlAttribute( AttributeName = "dead" )]
			public string Dead { get; set; }
		}

		[XmlRoot( ElementName = "joystick" )]
		public class Joystick
		{
			[XmlElement( ElementName = "axis" )]
			public Axis Axis { get; set; }
			[XmlAttribute( AttributeName = "is-axis" )]
			public string IsAxis { get; set; }
			[XmlAttribute( AttributeName = "button" )]
			public string Button { get; set; }
		}

		[XmlRoot( ElementName = "action" )]
		public class Action
		{
			[XmlElement( ElementName = "joystick" )]
			public Joystick Joystick { get; set; }
			[XmlAttribute( AttributeName = "id" )]
			public string Id { get; set; }
			[XmlAttribute( AttributeName = "keyboard-key" )]
			public string Keyboardkey { get; set; }
			[XmlAttribute( AttributeName = "policy" )]
			public string Policy { get; set; }
		}

		[XmlRoot( ElementName = "actions" )]
		public class Actions
		{
			[XmlElement( ElementName = "action" )]
			public List<Action> Action { get; set; }
			[XmlAttribute( AttributeName = "size" )]
			public string Size { get; set; }
		}

		[XmlRoot( ElementName = "horizontal" )]
		public class Horizontal
		{
			[XmlAttribute( AttributeName = "name" )]
			public string Name { get; set; }
			[XmlAttribute( AttributeName = "dead" )]
			public string Dead { get; set; }
			[XmlAttribute( AttributeName = "is-inverted" )]
			public string IsInverted { get; set; }
		}

		[XmlRoot( ElementName = "vertical" )]
		public class Vertical
		{
			[XmlAttribute( AttributeName = "name" )]
			public string Name { get; set; }
			[XmlAttribute( AttributeName = "dead" )]
			public string Dead { get; set; }
			[XmlAttribute( AttributeName = "is-inverted" )]
			public string IsInverted { get; set; }
		}

		[XmlRoot( ElementName = "joysticks" )]
		public class Joysticks
		{
			[XmlElement( ElementName = "horizontal" )]
			public Horizontal Horizontal { get; set; }
			[XmlElement( ElementName = "vertical" )]
			public Vertical Vertical { get; set; }
		}

		[XmlRoot( ElementName = "inputs" )]
		public class Inputs
		{
			[XmlElement( ElementName = "actions" )]
			public Actions Actions { get; set; }
			[XmlElement( ElementName = "joysticks" )]
			public Joysticks Joysticks { get; set; }
		}
	}
}