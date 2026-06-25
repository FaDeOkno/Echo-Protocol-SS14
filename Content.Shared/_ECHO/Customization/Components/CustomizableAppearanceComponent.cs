using Content.Shared.Body;
using Content.Shared.Humanoid;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._ECHO.Customization;

[RegisterComponent, NetworkedComponent]
public sealed partial class CustomizableAppearanceComponent : Component
{
    [DataField(required: true)]
    public List<HumanoidVisualLayers> AllowedLayers = new();

    [DataField(required: true)]
    public HashSet<ProtoId<OrganCategoryPrototype>> Organs;

    [DataField]
    public float AppearanceChangeDuration = 1f;

    [DataField]
    public float VoiceChangeDuration = 1f;

    [DataField]
    public bool AllowVoiceChange = false;

    public bool AllowAppearanceChange => AllowedLayers.Count > 0;

    [DataField(required: true)]
    public string ActionId = "";

    [ViewVariables(VVAccess.ReadOnly)]
    public EntityUid? MenuAction;
}
