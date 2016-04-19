using AutoMapper;
using AutoMapper.Mappers;
using Demo.Dto.Profiles;
using Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Dto
{
    public class AutoMapperConfiguration
    {
        private static MapperConfiguration mapperCfg;

        public static MapperConfiguration Instance
        {
            get
            {
                if (mapperCfg == null)
                {
                    mapperCfg = InitConfiguration();
                }
                return mapperCfg;
            }
        }

        //在此方法内加入自定义的Mapping规则设置
        private static MapperConfiguration InitConfiguration()
        {

            var config = new MapperConfiguration(cfg =>
                               {
                                   cfg.AddConditionalObjectMapper().Where((s, d) => d.Name == s.Name + "Dto");

                                   GetProfiles().ForEach(p => cfg.AddProfile(p));
                               });

            return config;
        }

        private static List<Profile> GetProfiles()
        {
            var profiles =
               AppDomain.CurrentDomain.GetAssemblies()
                  .SelectMany(a => a.GetTypes())
                  .Where(t => t.BaseType == typeof(Profile) && t.FullName != "AutoMapper.MapperConfiguration+NamedProfile")
                  .Select(t => Activator.CreateInstance(t) as Profile);

            return profiles.ToList();
        }


    }
}
