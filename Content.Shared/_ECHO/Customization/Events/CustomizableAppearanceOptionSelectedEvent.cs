using Content.Shared.Actions;
using Robust.Shared.Serialization;

namespace Content.Shared._ECHO.Customization;

[Serializable, NetSerializable]
public sealed partial class CustomizableAppearanceOptionSelectedEvent : EntityEventArgs
{
    public readonly NetEntity Sender;
    public readonly CustomizableAppearanceRadialOption Option;

    public CustomizableAppearanceOptionSelectedEvent(NetEntity sender, CustomizableAppearanceRadialOption option)
    {
        Sender = sender;
        Option = option;
    }
}
