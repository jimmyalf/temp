using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Web.Mvc;
using Spinit.Wpc.Synologen.Core.Domain.Model.FrameOrder;
using Spinit.Wpc.Synologen.Presentation.Helpers;
using Spinit.Wpc.Synologen.Presentation.Models;
using Spinit.Wpc.Synologen.Presentation.Test.Factories;

namespace Spinit.Wpc.Synologen.Presentation.Test
{
    [TestFixture, Category("FrameControllerSupplierTests")]
    public partial class Given_A_FrameController
    {
        [Test]
        public void When_Suppliers_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
        {
            //Arrange
            var gridPageSortParameters = new GridPageSortParameters();
            var expectedFirstItem = RepositoryFactory.GetMockedFrameSupplier(1);

            //Act
            var result = (ViewResult)controller.Suppliers(gridPageSortParameters);
            var model = (IEnumerable<FrameSupplierListItemView>)result.ViewData.Model;

            //Assert
            Expect(model.Count(), Is.EqualTo(10));
            Expect(model.First().Id, Is.EqualTo(expectedFirstItem.Id));
            Expect(model.First().Name, Is.EqualTo(expectedFirstItem.Name));
            Expect(model.First().Email, Is.EqualTo(expectedFirstItem.Email));
        }

        [Test]
        public void When_EditSupplier_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
        {
            //Arrange
            var domainItem = RepositoryFactory.GetMockedFrameSupplier(1);
            //Act
            var result = (ViewResult)controller.EditSupplier(1);
            var model = (FrameSupplierEditView)result.ViewData.Model;

            //Assert
            Expect(model, Is.Not.Null);
            Expect(model.FormLegend, Is.EqualTo("Redigera leverantör"));
            Expect(model.Id, Is.EqualTo(domainItem.Id));
            Expect(model.Name, Is.EqualTo(domainItem.Name));
            Expect(model.Email, Is.EqualTo(domainItem.Email));
        }

        [Test]
        public void When_EditSupplier_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
        {
            //Arrange
            var viewModel = ViewModelFactory.GetFrameSupplierEditView(9);
            const string expectedActionMessage = "Leverantören har sparats";
            //Act
            var result = (RedirectToRouteResult)controller.EditSupplier(viewModel);
            var actionMessage = controller.GetWpcActionMessages();
            var savedItem = ((RepositoryFactory.GenericMockRepository<FrameSupplier>)frameSupplierRepository).SavedEntity;

            //Assert
            Expect(savedItem, Is.Not.Null);
            Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
            Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
            Expect(savedItem.Email, Is.EqualTo(viewModel.Email));
            Expect(result.RouteValues["action"], Is.EqualTo("Suppliers"));
            Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
            Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
        }

        [Test]
        public void When_EditSupplier_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
        {
            //Arrange

            //Act
            controller.ModelState.AddModelError("*", "Invalid model state");
            var result = controller.EditSupplier(null);
            var viewResult = result as ViewResult ?? new ViewResult();
            var savedItem = ((RepositoryFactory.GenericMockRepository<FrameSupplier>)frameSupplierRepository).SavedEntity;

            //Assert
            Expect(savedItem, Is.Null);
            Expect(viewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Expect(result is RedirectToRouteResult, Is.False);
        }

        [Test]
        public void When_AddSupplier_GET_Is_Called_Returned_ViewModel_Has_Expected_Values()
        {
            //Arrange

            //Act
            var result = (ViewResult)controller.AddSupplier();
            var model = (FrameSupplierEditView)result.ViewData.Model;

            //Assert
            Expect(model, Is.Not.Null);
            Expect(model.FormLegend, Is.EqualTo("Skapa ny leverantör"));
            Expect(model.Id, Is.EqualTo(0));
            Expect(model.Name, Is.EqualTo(null));
            Expect(model.Email, Is.EqualTo(null));
        }

        [Test]
        public void When_AddSupplier_POST_Is_Called_Saved_DomainItem_Has_Expected_Values_And_Redirects()
        {
            //Arrange
            var viewModel = ViewModelFactory.GetFrameSupplierEditView(0);
            const string expectedActionMessage = "Leverantören har sparats";

            //Act
            var result = (RedirectToRouteResult)controller.AddSupplier(viewModel);
            var actionMessage = controller.GetWpcActionMessages();
            var savedItem = ((RepositoryFactory.GenericMockRepository<FrameSupplier>)frameSupplierRepository).SavedEntity;

            //Assert
            Expect(savedItem, Is.Not.Null);
            Expect(savedItem.Id, Is.EqualTo(viewModel.Id));
            Expect(savedItem.Name, Is.EqualTo(viewModel.Name));
            Expect(savedItem.Email, Is.EqualTo(viewModel.Email));
            Expect(result.RouteValues["action"], Is.EqualTo("Suppliers"));
            Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
            Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
        }

        [Test]
        public void When_AddSupplier_POST_With_Invalid_ModelState_Is_Called_Validation_Fails_And_Does_Not_Redirect()
        {
            //Arrange

            //Act
            controller.ModelState.AddModelError("*", "Invalid model state");
            var result = controller.AddSupplier(null);
            var viewResult = result as ViewResult ?? new ViewResult();
            var savedItem = ((RepositoryFactory.GenericMockRepository<FrameSupplier>)frameSupplierRepository).SavedEntity;

            //Assert
            Expect(savedItem, Is.Null);
            Expect(viewResult.ViewData.ModelState.IsValid, Is.EqualTo(false));
            Expect(result is RedirectToRouteResult, Is.False);
        }

        [Test]
        public void When_DeleteSupplier_POST_Is_Called_Deleted_DomainItem_Has_Expected_Values_And_Redirects()
        {
            //Arrange
            const int itemId = 1;
            const string expectedActionMessage = "Leverantören har raderats";

            //Act
            var result = (RedirectToRouteResult)controller.DeleteSupplier(itemId);
            var actionMessage = controller.GetWpcActionMessages();
            var deletedItem = ((RepositoryFactory.GenericMockRepository<FrameSupplier>)frameSupplierRepository).DeletedEntity;

            //Assert
            Expect(deletedItem, Is.Not.Null);
            Expect(deletedItem.Id, Is.EqualTo(itemId));
            Expect(result.RouteValues["action"], Is.EqualTo("Suppliers"));
            Expect(actionMessage.First().Message, Is.EqualTo(expectedActionMessage));
            Expect(actionMessage.First().Type, Is.EqualTo(WpcActionMessageType.Success));
        }

        [Test]
        public void When_DeleteSupplier_POST_Is_Called_With_An_Item_That_Has_Conncetions_An_ErrorMessage_Is_Registered_And_Redirects()
        {
            //Arrange

            //Act

            //Assert
        }


    }
}