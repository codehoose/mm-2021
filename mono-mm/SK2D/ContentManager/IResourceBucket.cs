namespace SK2D.ContentManager
{
    interface IResourceBucket
    {
        SK2DGame Game { get; }

        object Get(string name);
    }
}
