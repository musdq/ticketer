using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace E_Ticketer.Localization
{
    public static class E_TicketerLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(E_TicketerConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(E_TicketerLocalizationConfigurer).GetAssembly(),
                        "E_Ticketer.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
