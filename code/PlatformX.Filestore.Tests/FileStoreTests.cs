using Moq.AutoMock;

namespace PlatformX.Filestore.Tests
{
    public class FileStoreTests
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