namespace Content.Shared.Kitchen;

/// <summary>
/// Raised on an entity when it is inside a microwave and it starts cooking.
/// </summary>
public sealed class BeingMicrowavedEvent(EntityUid microwave, EntityUid? user, bool beingHeated, bool beingIrradiated) : HandledEntityEventArgs // Pe-tweak Было: public sealed class BeingMicrowavedEvent(EntityUid microwave, EntityUid? user) : HandledEntityEventArgs -> Стало: public sealed class BeingMicrowavedEvent(EntityUid microwave, EntityUid? user, bool beingHeated, bool beingIrradiated) : HandledEntityEventArgs
{
    public EntityUid Microwave = microwave;
    public EntityUid? User = user;
    public bool BeingHeated = beingHeated; // Pe-tweak добавлено BeingHeated = beingHeated;
    public bool BeingIrradiated = beingIrradiated; // Pe-tweak добавлено BeingIrradiated = beingIrradiated;
}
