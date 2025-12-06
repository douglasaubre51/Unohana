namespace Unohana.Utilities;

public static class MapperUtility
{
    public static Mapper GetMapper<TSource, TDest>()
    {
        var logger = new LoggerFactory();
        var config = new MapperConfiguration(cfg =>
            cfg.CreateMap<TSource, TDest>(),
            logger
        );

        return new Mapper(config);
    }
}
