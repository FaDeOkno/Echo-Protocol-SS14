using System.Linq;
using Content.Shared.Body;
using Content.Shared.Humanoid;
using Robust.Shared.GameStates;
using Robust.Shared.Prototypes;

namespace Content.Shared._ECHO.Customization;

public abstract class SharedCustomizableAppearanceSystem : EntitySystem
{
    [Dependency] private readonly SharedVisualBodySystem _visualBody = default!;
    [Dependency] private readonly SharedUserInterfaceSystem _ui = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CustomizableAppearanceComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<CustomizableAppearanceComponent, ToggleCustomizableAppearanceMenuEvent>(OnToggleMenu);

        Subs.BuiEvents<CustomizableAppearanceComponent>(CustomizableAppearanceUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnBuiOpened);
        });
    }

    private void OnInit(Entity<CustomizableAppearanceComponent> ent, ref ComponentInit args)
    {
        UpdateUi(ent);
    }

    private void OnToggleMenu(Entity<CustomizableAppearanceComponent> ent, ref ToggleCustomizableAppearanceMenuEvent args)
    {
        if (args.Handled)
            return;

        args.Handled = true;
        _ui.OpenUi(ent.Owner, CustomizableAppearanceUiKey.Key, args.Performer);
    }

    private void OnBuiOpened(Entity<CustomizableAppearanceComponent> ent, ref BoundUIOpenedEvent args)
        => UpdateUi(ent);

    private void OnChangeDoAfter(Entity<CustomizableAppearanceComponent> ent)
    {
    }

    private void UpdateUi(Entity<CustomizableAppearanceComponent> ent)
    {
        if (!_visualBody.TryGatherMarkingsData(ent.Owner, ent.Comp.AllowedLayers.ToHashSet(), out var profiles, out var markings, out var applied))
            return;

        foreach (var profile in profiles)
        {
            if (!ent.Comp.Organs.Contains(profile.Key))
                profiles.Remove(profile.Key);
        }

        foreach (var marking in markings)
        {
            if (!ent.Comp.Organs.Contains(marking.Key))
            {
                profiles.Remove(marking.Key);
                continue;
            }

            marking.Value.Layers.IntersectWith(ent.Comp.AllowedLayers);
        }

        foreach (var appliedPair in applied)
        {
            if (!ent.Comp.Organs.Contains(appliedPair.Key))
                applied.Remove(appliedPair.Key);
        }

        var state = new CustomizableAppearanceBoundUserInterfaceState(ent.Comp.AllowAppearanceChange, ent.Comp.AllowVoiceChange, profiles, markings, applied);
        _ui.SetUiState(ent.Owner, CustomizableAppearanceUiKey.Key, state);
    }
}
