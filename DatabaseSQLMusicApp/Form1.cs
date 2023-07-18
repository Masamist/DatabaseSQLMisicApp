using System.Windows.Forms;

namespace DatabaseSQLMusicApp
{
    public partial class Form1 : Form
    {
        BindingSource albumBindingSource = new BindingSource();
        BindingSource trackBindingSource = new BindingSource();

        List<Album> albums = new List<Album>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AlbumDAO albumDAO = new AlbumDAO();

            // connect the list to the grid view control
            albums = albumDAO.getAllAlbums();

            albumBindingSource.DataSource = albums;

            dataGridView1.DataSource = albumBindingSource;

            pictureBox1.Load("https://upload.wikimedia.org/wikipedia/en/thumb/e/e5/Beatles-singles-yesterday.jpg/220px-Beatles-singles-yesterday.jpg");

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AlbumDAO albumDAO = new AlbumDAO();

            // connect the list to the grid view control
            albums = albumDAO.getAllAlbums();

            albumBindingSource.DataSource = albumDAO.searchTitles(textBox1.Text);

            dataGridView1.DataSource = albumBindingSource;

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dataGridView = (DataGridView)sender;

            // get the row number clicked

            int rowClicked = dataGridView.CurrentRow.Index;
            // MessageBox.Show("You clicked row " + rowClicked);

            String imageURL = dataGridView.Rows[rowClicked].Cells[4].Value.ToString();
            // MessageBox.Show("URL="+ imageURL);

            pictureBox1.Load(imageURL);

            trackBindingSource.DataSource = albums[rowClicked].Tracks;
            dataGridView2.DataSource = trackBindingSource;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            // add a new item to the database

            Album album = new Album
            {
                AlbumName = txt_albumName.Text,
                ArtistName = txt_albumArtist.Text,
                Year = Int32.Parse(txt_albumYear.Text),
                ImageURL = txt_albumImageURL.Text,
                Description = txt_albumDescription.Text
            };

            AlbumDAO albumsDAO = new AlbumDAO();
            int result = albumsDAO.addOneAlbum(album);

            MessageBox.Show(result + " new row(s) inserted.");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            // get the row number clicked

            int rowClicked = dataGridView2.CurrentRow.Index;
            MessageBox.Show("You clicked row " + rowClicked);
            int trackID = (int)dataGridView2.Rows[rowClicked].Cells[0].Value;
            MessageBox.Show("IF of track: " + trackID);

            AlbumDAO albumDao = new AlbumDAO();

            int result = albumDao.deleteTrack(trackID);

            MessageBox.Show("Result " + result);

            dataGridView2.DataSource = null;
            albums = albumDao.getAllAlbums();
        }
    }
}