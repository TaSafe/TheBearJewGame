public interface IInteraction
{
    public enum InteractionType { GENERAL, GUN}
    public InteractionType MyType { get; set; }

    void Interaction();
}