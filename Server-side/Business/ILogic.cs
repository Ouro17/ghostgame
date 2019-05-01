namespace Business
{
    public interface ILogic<T>
    {
        ResultContainer<T> Play(T element);
    }
}