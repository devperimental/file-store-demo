using Moq.AutoMock;

namespace PlatformX.StorageProvider.Tests
{
    public class StorageProviderTests
    {
        private AutoMocker _mocker;

        [SetUp]
        public void Setup()
        {
            _mocker = new AutoMocker();
        }


        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}