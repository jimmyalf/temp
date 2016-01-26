using System.Linq;
using NUnit.Framework;
using Spinit.Wpc.Synologen.Data.Test.FrameData.Factories;
using Spinit.Wpc.Synologen.Integration.Data.Test.FrameData;

namespace Spinit.Wpc.Synologen.Data.Test.FrameData
{
    [TestFixture, Category("TestFrameSupplier")]
    public class Given_a_framesupplier : TestBase
    {
        [SetUp]
        public void Context()
        {
            SetupDefaultContext();
        }

        [Test]
        public void Can_get_persisted_framesupplier()
        {
            //Arrange

            //Act
            var savedFrameSupplier = SavedFrameSuppliers.First();
            var persistedFrameSupplier = FrameSupplierValidationRepository.Get(savedFrameSupplier.Id);

            //Assert
            Expect(persistedFrameSupplier, Is.Not.Null);
            Expect(persistedFrameSupplier.Id, Is.EqualTo(savedFrameSupplier.Id));
            Expect(persistedFrameSupplier.Name, Is.EqualTo(savedFrameSupplier.Name));
            Expect(persistedFrameSupplier.Email, Is.EqualTo(savedFrameSupplier.Email));
        }

        [Test]
        public void Can_edit_persisted_framesupplier()
        {
            //Arrange
            

            //Act
            var editedFrameSupplier = FrameSupplierFactory.ScrabmleFrameSupplier(SavedFrameSuppliers.First());
            FrameSupplierRepository.Save(editedFrameSupplier);
            var persistedFrameSupplier = FrameSupplierValidationRepository.Get(SavedFrameSuppliers.First().Id);

            //Assert
            Expect(persistedFrameSupplier, Is.Not.Null);
            Expect(persistedFrameSupplier.Id, Is.EqualTo(editedFrameSupplier.Id));
            Expect(persistedFrameSupplier.Name, Is.EqualTo(editedFrameSupplier.Name));
            Expect(persistedFrameSupplier.Email, Is.EqualTo(editedFrameSupplier.Email));
        }
    }
}