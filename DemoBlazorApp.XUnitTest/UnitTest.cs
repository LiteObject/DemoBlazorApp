namespace DemoBlazorApp.XUnitTest
{
    using System.Linq;
    using Library;
    using Models;
    using Services;
    using Xunit;

    public class UnitTest
    {
        [Fact]
        public void ValidModelTotalCalculation()
        {
            // ARRANGE
            IMathService mathService = new MathService();
            var threeNumberModel = new AddThreeNumbersModel(mathService) {Number1 = 1, Number2 = 1, Number3 = 1};

            // ACT
            var result = threeNumberModel.Total;

            // ASSERT
            Assert.Equal(3, result);
        }

        [Fact]
        public void ValidTableUpdateCalculation()
        {
            // ARRANGE
            IMathService mathService = new MathService();
            IDynamicTableService dynamicTableService = new DynamicTableService(mathService);
            dynamicTableService.SelectedTableType = new TableType() { Id = 1, Name = "AddThreeNumbersModel", Type = typeof(AddThreeNumbersModel) };
            var threeNumberModel = new AddThreeNumbersModel(mathService) { Number1 = 1, Number2 = 2, Number3 = 3 };
            
            // ACT
            var table = dynamicTableService.GetDynamicTable(threeNumberModel);
            var row = table.Rows[0];
            var updatedRow = (new AddThreeNumbersModel(mathService) { Number1 = 1, Number2 = 1, Number3 = 1 }).ToTableRow(0);
            table.Rows[0] = updatedRow;
            
            // ASSERT
            Assert.NotNull(table);
            Assert.NotNull(table.Rows);
            Assert.Single(table.Rows) ;
            var totalCellValue = table.Rows[0].Cells.FirstOrDefault(c => c.ColumnName.Equals(nameof(threeNumberModel.Total)))?.Value ?? "0" ;
            Assert.Equal(3, int.Parse(totalCellValue));
        }
    }
}
