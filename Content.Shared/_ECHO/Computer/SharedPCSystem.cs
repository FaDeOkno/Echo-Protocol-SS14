using Content.Shared.Item.ItemToggle;
using Content.Shared.Item.ItemToggle.Components;
using Content.Shared.Verbs;

namespace Content.Shared._ECHO.Computer;

public abstract class SharedPCSystem : EntitySystem
{
    [Dependency] protected readonly SharedUserInterfaceSystem UI = default!;
    [Dependency] protected readonly ItemToggleSystem Toggle = default!;

    public override void Initialize()
    {
        base.Initialize();

        SubscribeLocalEvent<PCComponent, GetVerbsEvent<AlternativeVerb>>(OnGetVerbs);
        SubscribeLocalEvent<PCComponent, ItemToggledEvent>(OnToggled);
    }

    private void OnGetVerbs(Entity<PCComponent> ent, ref GetVerbsEvent<AlternativeVerb> args)
    {
        var user = args.User;
        args.Verbs.Add(new()
        {
            Act = () => ToggleComputer(ent, user),
            Text = Loc.GetString($"alt-verb-computer-toggle-{(!Toggle.IsActivated(ent.Owner)).ToString().ToLower()}")
        });
    }

    private void OnToggled(Entity<PCComponent> ent, ref ItemToggledEvent args)
    {
        UpdateUi(ent);
    }

    public void ToggleComputer(Entity<PCComponent> ent, EntityUid user)
    {
        Toggle.Toggle(ent.Owner, user);
    }

    public virtual void UpdateUi(Entity<PCComponent> ent)
    {
    }
}
