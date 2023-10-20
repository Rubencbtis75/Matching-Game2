using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matching_Game2
{
    public partial class Form1 : Form
    {     // Utilice este objeto aleatorio para elegir iconos aleatorios para los cuadrados
        Random random = new Random();
        Label firstClicked = null;

        //secondClicked apunta al segundo control Etiqueta
        //  Que la jugadora haga click
        Label secondClicked = null;

        //Cada una de estas letras es un icono interesante.
        // en la fuente Webdings,
        // y cada icono aparece dos veces en esta lista
        List<string> icons = new List<string>()
    {
        "t", "t", "p", "p", "y", "y", "l", "l",
        "d", "d", "e", "e", "m", "m", "o", "o"
    };
        private void AssignIconsToSquares()
        {
            // El TableLayoutPanel tiene 16 etiquetas,
            // y la lista de íconos tiene 16 íconos,
            // entonces se extrae un ícono al azar de la lista
            // y agregado a cada etiqueta
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    // iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                    iconLabel.ForeColor = iconLabel.BackColor;
                }
            }
        }

       
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // El cronómetro sólo se activa después de dos
            // Se han mostrado iconos al jugador. 
            // así que ignore cualquier clic si el cronómetro está funcionando
            if (timer1.Enabled == true)
                return;

            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Si la etiqueta en la que se hizo clic es negra, el jugador hizo clic
                // un icono que ya ha sido revelado...
                // ignora el clic
                if (clickedLabel.ForeColor == Color.Red)
                    return;

                // Si firstClicked es nulo, este es el primer icono
                // en el par en el que el jugador hizo clic, 
                //así que configure firstClicked en la etiqueta que el reproductor 
                // hace clic, cambia su color a negro y regresa
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Red;
                    return;
                }

                // Si el jugador llega tan lejos, el temporizador no lo es.
                // Correr y firstClicked no es nulo,
                // Entonces este debe ser el segundo ícono en el que el jugador hizo clic.
                // Establece su color en negro.
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Red;


                // Comprueba si el jugador ganó.
                CheckForWinner();

                // Si el jugador hizo clic en dos íconos iguales, mantenlos 
                // Negro y restablecer primeroHaga clic y segundoHaga clic 
                // Para que la jugadora pueda hacer clic en otro icono
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }


                // Si el jugador llega tan lejos, el jugador 
                // Hice clic en dos íconos diferentes, así que inicie el
                //temporizador (que esperará tres cuartos de
                // un segundo y luego ocultar los iconos)
                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // detener el cronómetro
            timer1.Stop();

            // Ocultar ambos iconos
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            //Restablecer el primer clic y el segundo clic
            //así la próxima vez que se coloque una etiqueta
            // Cuando se hace clic, el programa sabe que es el primer clic.
            firstClicked = null;
            secondClicked = null;
        }
        private void CheckForWinner()
        {
            // Revise todas las etiquetas en TableLayoutPanel, 
            // comprobando cada uno para ver si su icono coincide
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // Si el bucle no regresó, no encontró
            // cualquier ícono inigualable
            // Eso significa que el usuario ganó. Mostrar un mensaje y cerrar el formulario.
            MessageBox.Show("¡Hiciste coincidir todos los íconos!", "Felicidades");
            Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
