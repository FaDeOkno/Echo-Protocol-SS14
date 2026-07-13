using Content.Shared.Damage.Prototypes;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._ECHO.Radiation;

[RegisterComponent, NetworkedComponent, AutoGenerateComponentState]
public sealed partial class RadiationOnDamageComponent : Component
{
    [DataField, AutoNetworkedField]
    public Dictionary<ProtoId<DamageTypePrototype>, float> IntensityPerDamage = new();

    [DataField, AutoNetworkedField]
    public float MaxIntensity = 1f;
}
