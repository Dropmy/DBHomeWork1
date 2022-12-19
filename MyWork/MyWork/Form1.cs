namespace MyWork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            DataGridViewDbLoad();
        }

        private void DataGridViewDbLoad()
        {
            using (CategoryContext db = new CategoryContext())
            {
                //Если нет ни одного элемента в бд
                if (!db.Categories.Any())
                {
                    //Добавляем и сохраняем данные
                    db.Categories.Add( new CategoryModel { Category = "Some Category", Discount = 13 } );
                    db.SaveChanges();
                }

                //Отображаем текущее состояние бд 
                dataGridView1.DataSource = db.Categories.ToList<CategoryModel>();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string type = textBox2.Text;
                int discount = Convert.ToInt32(textBox3.Text);

                using (CategoryContext db = new CategoryContext())
                {
                    //Создаем модель для добавления в бд
                    CategoryModel categoryToAdd = new CategoryModel { Category = type, Discount = discount };

                    //Добавляем и сохраняем данные
                    db.Categories.Add(categoryToAdd);
                    db.SaveChanges();

                    //Отображаем текущее состояние бд 
                    dataGridView1.DataSource = db.Categories.ToList<CategoryModel>();
                }
            }
            catch {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //Попытка привести к нужному типу
                int id = Convert.ToInt32(textBox1.Text);
                string type = textBox2.Text;
                int discount = Convert.ToInt32(textBox3.Text);

                using (CategoryContext db = new CategoryContext())
                {
                    CategoryModel categoryToAdd = new CategoryModel { Category = type, Discount = discount, Id = id };
                    try
                    {
                        //Пытаемся обновить данные
                        db.Categories.Update(categoryToAdd);
                        db.SaveChanges();

                        //Отображаем текущее состояние бд 
                        dataGridView1.DataSource = db.Categories.ToList<CategoryModel>();
                    }
                    catch
                    {
                        //Отображаем сообщение об ошибке
                        MessageBox.Show("Невозможно обновить данных которых еще нет в БД");
                    }
                }
            }
            catch 
            {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt32(textBox1.Text);

                using (CategoryContext db = new CategoryContext())
                {
                    //Пытаемся найти категорию на удаление из бд
                    CategoryModel? categoryToRemove = db.Categories.FirstOrDefault<CategoryModel?>(category => category.Id == id);
                    if (categoryToRemove != null)
                    {
                        //Удаляем и сохраняем
                        db.Categories.Remove(categoryToRemove);
                        db.SaveChanges();

                        //Отображаем текущее состояние бд 
                        dataGridView1.DataSource = db.Categories.ToList<CategoryModel>();
                    }
                }
            }
            catch {
                //Отображаем сообщение об ошибке
                MessageBox.Show("Данные введены некоректно");
                return;
            }
        }
    }
}
