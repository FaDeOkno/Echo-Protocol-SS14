using Content.Shared.Body;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization;

namespace Content.Shared._ECHO.Customization;

[Serializable, NetSerializable]
public sealed partial class CustomizableAppearanceBoundUserInterfaceState : BoundUserInterfaceState
{
    public bool AllowLayers;
    public bool AllowVoiceChange;

    public Dictionary<ProtoId<OrganCategoryPrototype>, OrganProfileData> OrganProfileData;
    public Dictionary<ProtoId<OrganCategoryPrototype>, OrganMarkingData> OrganMarkingData;
    public Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> AppliedMarkings;

    public CustomizableAppearanceBoundUserInterfaceState(bool layers, bool voice,
                                                         Dictionary<ProtoId<OrganCategoryPrototype>, OrganProfileData> profiles,
                                                         Dictionary<ProtoId<OrganCategoryPrototype>, OrganMarkingData> markings,
                                                         Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> applied)
    {
        AllowLayers = layers;
        AllowVoiceChange = voice;
        OrganProfileData = profiles;
        OrganMarkingData = markings;
        AppliedMarkings = applied;
    }
}

[Serializable, NetSerializable]
public enum CustomizableAppearanceUiKey
{
    Key
}
