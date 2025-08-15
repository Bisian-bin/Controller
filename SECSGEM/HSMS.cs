using InnerEvents;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SECSGEM
{
    public class HSMS
    {
        private IEventAggregator eventAggregator;
        private IUnityContainer container;

        public HSMS(IEventAggregator eventAggregator, IUnityContainer container)
        {
            this.eventAggregator = eventAggregator;
            this.container = container;

            SECSGEMEvent secs = eventAggregator.GetEvent<SECSGEMEvent>();
            secs.Subscribe((o) =>
            {
                eventAggregator.GetEvent<MainEvent>().Publish(o);
            }, ThreadOption.BackgroundThread);
        }
    }
}
