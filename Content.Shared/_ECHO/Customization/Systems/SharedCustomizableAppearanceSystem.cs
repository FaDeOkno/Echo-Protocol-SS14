using System.Linq;
using Content.Shared.Body;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Markings;
using Robust.Shared.GameStates;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Timing;

namespace Content.Shared._ECHO.Customization;

public abstract class SharedCustomizableAppearanceSystem : EntitySystem
{
    [Dependency] protected readonly SharedVisualBodySystem VisualBody = default!;
    [Dependency] private readonly SharedUserInterfaceSystem _ui = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<CustomizableAppearanceComponent, ComponentInit>(OnInit);
        SubscribeLocalEvent<CustomizableAppearanceComponent, ToggleCustomizableAppearanceMenuEvent>(OnToggleMenu);

        SubscribeAllEvent<CustomizableAppearanceOptionSelectedEvent>(OnRadialOptionSelected);

        Subs.BuiEvents<CustomizableAppearanceComponent>(CustomizableAppearanceUiKey.Key, subs =>
        {
            subs.Event<BoundUIOpenedEvent>(OnBuiOpened);
            subs.Event<CustomizableAppearanceSelectMarkingMessage>(OnSelectMarking);
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

        if (ent.Comp.RadialOptions.Count <= 0)
        {
            _ui.OpenUi(ent.Owner, CustomizableAppearanceUiKey.Key, args.Performer);
        }
        else if (ent.Comp.RadialOptions.Count == 1)
        {
            RaiseLocalEvent(new CustomizableAppearanceOptionSelectedEvent(GetNetEntity(ent.Owner), ent.Comp.RadialOptions[0]));
        }
        else
        {
            OpenRadialMenu(ent.Comp.RadialOptions);
        }
    }

    private void OnRadialOptionSelected(CustomizableAppearanceOptionSelectedEvent args)
    {
        var ent = GetEntity(args.Sender);

        if (args.Option.UiKey != null)
        {
            _ui.TryOpenUi(ent, args.Option.UiKey, ent);
        }

        if (args.Option.Event != null)
        {
            RaiseLocalEvent(ent, args.Option.Event);
        }
    }

    private void OnSelectMarking(Entity<CustomizableAppearanceComponent> ent, ref CustomizableAppearanceSelectMarkingMessage args)
    {
        if (ent.Comp.AppearanceChangeDuration <= 0f)
        {
            VisualBody.ApplyMarkings(ent.Owner, args.Markings);
            UpdateUi(ent);
        }
        else
        {
            StartChangeDoAfter(ent, args.Markings);
        }
    }

    private void OnBuiOpened(Entity<CustomizableAppearanceComponent> ent, ref BoundUIOpenedEvent args)
        => UpdateUi(ent);

    protected virtual void StartChangeDoAfter(Entity<CustomizableAppearanceComponent> ent, Dictionary<ProtoId<OrganCategoryPrototype>, Dictionary<HumanoidVisualLayers, List<Marking>>> markings)
    {
    }

    protected virtual void OpenRadialMenu(List<CustomizableAppearanceRadialOption> options)
    {
    }

    protected void UpdateUi(Entity<CustomizableAppearanceComponent> ent)
    {
        if (!VisualBody.TryGatherMarkingsData(ent.Owner, ent.Comp.AllowedLayers.ToHashSet(), out var profiles, out var markings, out var applied))
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
