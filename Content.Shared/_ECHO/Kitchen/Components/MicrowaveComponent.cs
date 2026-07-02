using Content.Shared.Construction.Prototypes;
using Content.Shared.DeviceLinking;
using Content.Shared.Item;
using Robust.Shared.Audio;
using Robust.Shared.Containers;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Shared.Kitchen.Components;

[RegisterComponent]
public sealed partial class MicrowaveComponent : Component
{
    [DataField("cookTimeMultiplier"), ViewVariables(VVAccess.ReadWrite)]
    public float CookTimeMultiplier = 1;

    [ViewVariables(VVAccess.ReadOnly)]
    public float FinalCookTimeMultiplier = 1.0f;

    [DataField("cookTimeScalingConstant")]
    public float CookTimeScalingConstant = 0.5f;

    [DataField("baseHeatMultiplier"), ViewVariables(VVAccess.ReadWrite)]
    public float BaseHeatMultiplier = 100;

    [DataField("objectHeatMultiplier"), ViewVariables(VVAccess.ReadWrite)]
    public float ObjectHeatMultiplier = 100;

    [DataField("failureResult", customTypeSerializer: typeof(PrototypeIdSerializer<EntityPrototype>))]
    public string BadRecipeEntityId = "FoodBadRecipe";

    [DataField("beginCookingSound")]
    public SoundSpecifier StartCookingSound = new SoundPathSpecifier("/Audio/Machines/microwave_start_beep.ogg");

    [DataField("foodDoneSound")]
    public SoundSpecifier FoodDoneSound = new SoundPathSpecifier("/Audio/Machines/microwave_done_beep.ogg");

    [DataField("clickSound")]
    public SoundSpecifier ClickSound = new SoundPathSpecifier("/Audio/Machines/machine_switch.ogg");

    [DataField("ItemBreakSound")]
    public SoundSpecifier ItemBreakSound = new SoundPathSpecifier("/Audio/Effects/clang.ogg");

    public EntityUid? PlayingStream;

    [DataField("loopingSound")]
    public SoundSpecifier LoopingSound = new SoundPathSpecifier("/Audio/Machines/microwave_loop.ogg");

    [ViewVariables]
    public bool Broken;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public ProtoId<SinkPortPrototype> OnPort = "On";

    [DataField("currentCookTimerTime"), ViewVariables(VVAccess.ReadWrite)]
    public uint CurrentCookTimerTime = 0;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public TimeSpan CurrentCookTimeEnd = TimeSpan.Zero;

    [DataField("maxCookTime"), ViewVariables(VVAccess.ReadWrite)]
    public uint MaxCookTime = 30;

    [DataField("temperatureUpperThreshold")]
    public float TemperatureUpperThreshold = 373.15f;

    public int CurrentCookTimeButtonIndex;

    public Container Storage = default!;

    [DataField]
    public string ContainerId = "microwave_entity_container";

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public int Capacity = 10;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public ProtoId<ItemSizePrototype> MaxItemSize = "Normal";

    [DataField]
    public float MalfunctionInterval = 1.0f;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float ExplosionChance = .1f;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float LightningChance = .75f;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public bool CanMicrowaveIdsSafely = true;

    [DataField(customTypeSerializer: typeof(FlagSerializer<MicrowaveRecipeTypeFlags>)), ViewVariables(VVAccess.ReadWrite)]
    public int ValidRecipeTypes = (int)MicrowaveRecipeType.Microwave;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public bool CanHeat = true;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public bool CanIrradiate = true;

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public string TooBigPopup = "microwave-component-interact-item-too-big";

    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public SoundSpecifier NoRecipeSound = new SoundPathSpecifier("/Audio/Effects/Cargo/buzz_sigh.ogg");

    [DataField, ViewVariables(VVAccess.ReadOnly)]
    public MicrowaveUiKey Key = MicrowaveUiKey.Key;
}