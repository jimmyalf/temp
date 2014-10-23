using System;
using System.Collections.Generic;
using System.Configuration;

namespace Spinit.Wpc.Synologen.OPQ.Site.Code.Config
{
    public abstract class OPQConfigurationElementCollection<TConfigurationElement> : ConfigurationElementCollection, IEnumerable<TConfigurationElement>
        where TConfigurationElement : ConfigurationElement
    {
        private readonly Func<TConfigurationElement, object> _keySelector;

        protected OPQConfigurationElementCollection(Func<TConfigurationElement, object> keySelector)
        {
            _keySelector = keySelector;
        }

        public TConfigurationElement this[int index]
        {
            get
            {
                return BaseGet(index) as TConfigurationElement;
            }

            set
            {
                if (BaseGet(index) != null)
                {
                    BaseRemoveAt(index);
                }

                BaseAdd(index, value);
            }
        }

        public new IEnumerator<TConfigurationElement> GetEnumerator()
        {
            var list = new List<TConfigurationElement>();
            for (var i = 0; i < Count; i++)
            {
                list.Add(this[i]);
            }

            return list.GetEnumerator();
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return Activator.CreateInstance<TConfigurationElement>();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return _keySelector((TConfigurationElement)element);
        }
    }
}