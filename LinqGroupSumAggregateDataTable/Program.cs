using System.Data;

namespace LinqGroupSumAggregateDataTable
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DataTable dt = new DataTable("tblEntTable");
            dt.Columns.Add("ID", typeof(string));
            dt.Columns.Add("amount", typeof(decimal));
            dt.Rows.Add(new object[] {
                "1",
                100.51
            });
            dt.Rows.Add(new object[] {
                "1",
                200.52
            });
            dt.Rows.Add(new object[] {
                "2",
                500.24
            });
            dt.Rows.Add(new object[] {
                "2",
                400.31
            });
            dt.Rows.Add(new object[] {
                "3",
                600.88
            });
            dt.Rows.Add(new object[] {
                "3",
                700.11
            });

            var results = (from orders in dt.AsEnumerable()
                           group orders by orders.Field<string>("ID") into g
                           select new
                           {
                               ID = g.Key,
                               Amount = g.Sum(r => r.Field<decimal>("amount"))
                           }).OrderBy(tkey => tkey.ID).ToList();

            Console.WriteLine($"ID\tAmount");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.ID}\t{result.Amount.ToString("N")}");
            }
        }
    }
}