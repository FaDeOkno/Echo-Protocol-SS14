using Content.Shared.Popups;

namespace Content.Shared._ECHO.Light;

public abstract class SharedPointLightFollowLayerColorSystem : EntitySystem
{
    [Dependency] private SharedPopupSystem _popup = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PointLightFollowLayerColorComponent, TogglePointLightFollowLayerEvent>(OnToggle);
    }

    private void OnToggle(Entity<PointLightFollowLayerColorComponent> ent, ref TogglePointLightFollowLayerEvent args)
    {
        ent.Comp.Enabled = !ent.Comp.Enabled;
        Dirty(ent);

        _popup.PopupPredicted(Loc.GetString($"point-light-follow-toggle-{ent.Comp.Enabled.ToString().ToLower()}"), "", ent.Owner, ent.Owner);
    }
}
