namespace WebApplication1
{
    public class DataBaseHandler
    {

        public void ViewData()
        {
            KripnoteContext db = new KripnoteContext();
            var users = db.Data.ToList();
            Console.WriteLine("Data:");
            foreach (Datum u in users)
            {
                Console.WriteLine($"{u.IdD} {u.TimeD} {u.TempD} {u.MsgD}");
            }
        }
        public List<Datum> GetData()
        {
            KripnoteContext db = new KripnoteContext();
            return db.Data.ToList();
        }
        public void InsertData(Datum data) {
            KripnoteContext db = new KripnoteContext();
            db.Data.Add(data);
            db.SaveChanges();
        }
        public int MaxId()
        {
            KripnoteContext db = new KripnoteContext();

            var users = db.Data.ToList();

            int max = 0;
            if (users.Count > 0)
                foreach (Datum u in users)
                {
                    if (u.IdD > max)
                        max = u.IdD;
                }
            return max;
        }
    }
}
