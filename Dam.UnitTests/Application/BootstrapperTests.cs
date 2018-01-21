using Dam.Application;
using NUnit.Framework;

namespace Dam.UnitTests.Application
{
    [TestFixture]
    public class BootstrapperTests
    {
        [Test]
        public void StartsAndStops()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Start();
            bootstrapper.Dispose();
        }
    }
}