using DataImporter.Core.Abstractions;
using DataImporter.Data.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.IO;
using System.Text;

namespace DataImporter.Core.Tests
{
    [TestClass]
    public class DataImporterServiceTest
    {

        //Mock Services using Moq
        private Mock<ICompanyService> companyService;
        private Mock<IFeedService> feedService;
        private Mock<IProductService> productService;
        private Mock<IFileManager> fileManager;

        //Service Under Test
        private DataImporterService dataImporterService;


        private string ValidFolderLocation { get; set; }
        private string InValidFolderLocation { get; set; }

        /// <summary>
        /// Initialize and Setup method for returning default values when services are mocked.
        /// </summary>
        [TestInitialize]
        public void SetUp()
        {
            this.ValidFolderLocation = @"C:\Workspace\DataImporter\DataImporter.ConsoleApp\TestData";
            this.InValidFolderLocation = @"C:\Workspace\DataImporter\DataImporter.ConsoleApp\NoFolder";

            this.companyService = new Mock<ICompanyService>();
            this.feedService = new Mock<IFeedService>();
            this.productService = new Mock<IProductService>();
            this.fileManager = new Mock<IFileManager>();
            this.dataImporterService = new DataImporterService(productService.Object, companyService.Object, feedService.Object, fileManager.Object);


            this.feedService.Setup(a => a.GetFeedByName(It.IsAny<string>())).ReturnsAsync(new FeedEntity());
            this.feedService.Setup(a => a.AddFeed(It.IsAny<FeedEntity>())).ReturnsAsync(new FeedEntity());
            this.companyService.Setup(a => a.GetCompanyByName(It.IsAny<string>())).ReturnsAsync(new CompanyEntity());
            this.companyService.Setup(a => a.AddCompany(It.IsAny<CompanyEntity>())).ReturnsAsync(new CompanyEntity());
            this.productService.Setup(a => a.InsertProduct(It.IsAny<ProductEntity>()));

            this.fileManager.Setup(a => a.GetParent(It.IsAny<string>())).Returns(new DirectoryInfo(ValidFolderLocation));
            this.fileManager.Setup(a => a.GetFiles(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<SearchOption>())).Returns(new string[] { ValidFolderLocation });

        }

        //MethodName_ExpectedBehavior_StateUnderTest

        [TestMethod]
        public void CheckFileLocation_ReturnsTrue_IfFolderExists()
        {
            //Arrange
            SetFileContents("Unique Id,Name,Brand,Description\n1,Test Name 1,Test Brand 1,Test Description 1");

            //Act
            var result = dataImporterService.ProductImporter(ValidFolderLocation);

            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void CheckFileLocation_ReturnsFalse_IfFolderDoesNotExists()
        {
            var result = dataImporterService.ProductImporter(InValidFolderLocation);
            Assert.IsFalse(result);
        }

        [TestMethod]
        [DataRow("Unique Id,Name,Brand,Description\n1,Test Name 1,Test Brand 1,Test Description 1")]
        public void CheckValidColumnNames_NoException_IfColumnNamesValid(string validColumnNames)
        {
            SetFileContents(validColumnNames);
            AssertEx.NoExceptionThrown<Exception>(() => dataImporterService.ProductImporter(ValidFolderLocation));
        }

        [TestMethod]
        [DataRow("UniqueId, Name2, Brand3, Description5\n1, Test Name 1, Test Brand 1, Test Description 1")]
        public void CheckValidColumnNames_ThrowsException_IfColumnNamesNotValid(string notValidColumnNames)
        {
            SetFileContents(notValidColumnNames);
            Assert.ThrowsException<CsvHelper.MissingFieldException>(() => dataImporterService.ProductImporter(ValidFolderLocation));
        }

        [TestMethod]
        [DataRow("Unique Id,Name,Brand,Description\nTest,Test Name 1,Test Brand 1,Test Description 1")]
        public void CheckUniqueId_ThrowsException_IfDataTypeIsNotValid(string inValidUniqueIdData)
        {
            SetFileContents(inValidUniqueIdData);
            Assert.ThrowsException<CsvHelper.TypeConversion.TypeConverterException>(() => dataImporterService.ProductImporter(ValidFolderLocation));
        }

        [TestMethod]
        [DataRow("Unique Id,Name,Brand,Description\n1,Test Name 1,Test Brand 1,Test Description 1")]
        public void CheckUniqueId_ThrowsNoException_IfDataTypeIsValid(string validUniqueIdData)
        {
            SetFileContents(validUniqueIdData);
            AssertEx.NoExceptionThrown<Exception>(() => dataImporterService.ProductImporter(ValidFolderLocation));
        }

        private void SetFileContents(string fileContent)
        {
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes(fileContent);
            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
            this.fileManager.Setup(a => a.StreamReader(It.IsAny<string>())).Returns(() => new StreamReader(fakeMemoryStream));
        }
    }
}
