namespace Windsor.SLExample.Factories
{
    public interface IModelFactory
    {
        TModel CreateModel<TModel>();
        void FreeUpModel(object oldModel);
    }
}