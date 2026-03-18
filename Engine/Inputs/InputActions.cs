using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Latte;

public sealed class InputAction
{
	[XmlElement("Actions")]
	public List<IInputAction> Actions { get; set; }

	[XmlElement("ActionName")]
	public string Name { get; set; }
}

[XmlRoot("GameInputActions")]
public sealed class InputActions
{
	[XmlElement("InputActions")]
	public List<InputAction> Actions { get; set; }
}


