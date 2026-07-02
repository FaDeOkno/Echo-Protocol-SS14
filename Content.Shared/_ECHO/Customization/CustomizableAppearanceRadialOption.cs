using Robust.Shared.Graphics;
using Robust.Shared.Serialization;
using Robust.Shared.Utility;

namespace Content.Shared._ECHO.Customization;

[Serializable, NetSerializable, DataDefinition]
public sealed partial class CustomizableAppearanceRadialOption
{
    [DataField(required: true)]
    public SpriteSpecifier Icon = default!;

    [DataField(required: true)]
    public string OptionName = default!;

    [DataField]
    public Enum? UiKey = null;

    [DataField]
    public CustomizableAppearanceEvent? Event;
}

[ImplicitDataDefinitionForInheritors]
public abstract partial class CustomizableAppearanceEvent : EntityEventArgs
{
}
