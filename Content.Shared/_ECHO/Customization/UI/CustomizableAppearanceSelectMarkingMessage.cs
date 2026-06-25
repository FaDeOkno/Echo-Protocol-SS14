using Content.Shared.Body;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._ECHO.Customization;

[Serializable, NetSerializable]
public sealed class CustomizableAppearanceSelectMarkingMessage : BoundUserInterfaceMessage
{
    public CustomizableAppearanceSelectMarkingMessage(Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> markings)
    {
        Markings = markings;
    }

    public Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> Markings { get; }
}
