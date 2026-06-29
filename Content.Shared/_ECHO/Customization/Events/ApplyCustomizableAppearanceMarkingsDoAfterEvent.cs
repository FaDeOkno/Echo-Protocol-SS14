using Content.Shared.Actions;
using Content.Shared.Body;
using Content.Shared.DoAfter;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._ECHO.Customization;

[Serializable, NetSerializable]
public sealed partial class ApplyCustomizableAppearanceMarkingsDoAfterEvent : SimpleDoAfterEvent
{
    public readonly Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> Markings;

    public ApplyCustomizableAppearanceMarkingsDoAfterEvent(Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> markings)
    {
        Markings = markings;
    }
}
