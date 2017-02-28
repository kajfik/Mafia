using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace Mafia
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// numOfSpecificCards describes how many types of cards are there
        /// each card is specified by cardType
        /// if there are more cards of 1 type, they are distinguished by cardNumber
        /// 
        /// each card is then referred to as cards[cardType].card[cardNumber]
        /// </summary>

        const int numOfSpecificCards = 37;
        int[] amountOfSpecificCards = new int[numOfSpecificCards]
        {
         7, //mrakoszlap,
         2, //imunita
         3, //prowazochodec
         2, //neprustrzelno westa
         1, //matrix,
         1, //jailer,
         3, //mag,
         2, //slina,
         1, //pijavica,
         1, //piosek,
         1, //kobra,
         1, //magnet,
         1, //fusekla,
         1, //duchBobo,
         3, //mafian,
         2, //szilenyStrzelec
         1, //sniper,
         5, //zwierciadlo
         1, //terorista,
         1, //astronom,
         1, //meciar,
         1, //kovac,
         1, //alCapone,
         1, //gandalf,
         1, //kusKona,
         1, //ateista,
         1, //anarchista,
         1, //sklenar,
         1, //masowyWrah,
         1, //soudce,
         1, //slepyKat,
         1, //komunista,
         1, //luneta,
         1, //grabarz,
         1, //panCzasu,
         1, //doktor,
         1  //jozinZBazin
        };

        readonly string[,] cardNames = new string[numOfSpecificCards, 2]
        {
            {"mrakoszlap", "mrákošlap"},
            {"imunita", "imunita"},
            {"prowazochodec", "provazochodec"},
            {"kewlar", "kevlar"},
            {"matrix", "matrix"},
            {"jailer", "jailer"},
            {"mag", "mág"},
            {"slina", "slina"},
            {"pijavica", "pijavice"},
            {"piosek", "písek"},
            {"kobra", "kobra"},
            {"magnet", "magnet"},
            {"fusekla", "ponožka"},
            {"duch bobo", "duch bobo"},
            {"mafian", "mafián"},
            {"szileny strzelec", "šílený střelec"},
            {"sniper", "sniper"},
            {"zwierciadlo", "zrcadlo"},
            {"terorista", "terorista"},
            {"astronom", "astronom"},
            {"meciar", "mečiar"},
            {"kovac", "kováč"},
            {"al capone", "al capone"},
            {"gandalf", "gandalf"},
            {"kus kona", "kus koňa"},
            {"ateista", "ateista"},
            {"anarchista", "anarchista"},
            {"szklorz", "sklenář"},
            {"masowy zabijak", "masový zabiják"},
            {"soudce", "soudce"},
            {"slepy kat", "slepý kat"},
            {"komunista", "komunista"},
            {"luneta", "luneta"},
            {"grabarz", "hrobař"},
            {"pan czasu", "pán času"},
            {"doktor", "doktor"},
            {"jozin z bazin", "jožin z bažin"}
        };

        readonly int[] cardNightPhases = new int[numOfSpecificCards]
        {
             -1, //mrakoszlap,
             -1, //imunita
             -1, //prowazochodec
             -1, //neprustrzelno westa
             2,  //matrix,
             0,  //jailer,
             3,  //mag,
             6,  //slina,
             8,  //pijavica,
             9,  //piosek,
             10, //kobra,
             11, //magnet,
             21, //fusekla,
             12, //duchBobo,
             15, //mafian,
             18, //szilenyStrzelec
             20, //sniper,
             -1, //zwierciadlo
             -1, //terorista,
             -1, //astronom,
             -1, //meciar,
             -1, //kovac,
             -1, //alCapone,
             -1, //gandalf,
             -1, //kusKona,
             -1, //ateista,
             -1, //anarchista,
             -1, //sklenar,
             -1, //masowyWrah,
             22, //soudce,
             23, //slepyKat,
             -1, //komunista,
             24, //luneta,
             1,  //grabarz,
             -1, //panCzasu,
             13, //doktor,
             14  //jozinZBazin
        };

        /*
         * cards:
         * 0 - mrakoszlap - 7x
         * 1 - imunita - 2x
         * 2 - prowazochodec - 3x
         * 3 - neprustrzelno westa - 2x
         * 4 - matrix - 1 pouziti
         * 5 - jailer - co drugo noc - 1 pouziti
         * 6 - mag - 3x
         * 7 - slina - 2x
         * 8 - pijavica
         * 9 - piosek
         * 10 - kobra - co drugo noc
         * 11 - magnet - co drugo noc
         * 12 - fusekla - 1 pouziti
         * 13 - duch bobo - co drugo noc
         * 14 - mafian - 3x
         * 15 - szileny strzelec - 2x - co drugo noc
         * 16 - sniper - 1 pouziti
         * 17 - zwierciadlo - 5x
         * 18 - terorista
         * 19 - astronom
         * 20 - meciar
         * 21 - kovac
         * 22 - al capone
         * 23 - gandalf
         * 24 - kus kona
         * 25 - ateista
         * 26 - anarchista
         * 27 - sklenar
         * 28 - masowy wrah
         * 29 - soudce
         * 30 - slepy kat - 2 pouziti
         * 31 - komunista
         * 32 - luneta
         * 33 - grabarz
         * 34 - pan czasu 
         * 35 - doktor
         * 36 - jozin z bazin - 3 pouziti
         * Dokupy 57 karet
         */

        [Serializable]public class Player
        {
            public vector2D position = new vector2D();
            public bool alive = true;
            public List<int> cardNumbers;
            public List<int> cardTypes;
            public string name;
            public bool wake = true;
            public bool tunnelFrom = false;
            public bool hasSlina;
            public bool hasPiosek;
            public bool hasPijavica;
            public bool hasKobra;
            public bool hasMagnet;
            public bool hasExhib;
            public bool hasZakazGlosowanio;
            public bool hasDoktor;
            public bool ofiaraKata;
            public bool zachronionyKatym;
            public int tunnelsFrom;
            public int numberOfBloto;
            public List<Tunnel> tunnels;
        }

        [Serializable]public class Bullet
        {
            public bool usedMagnet = false;
            public bool[] usedTunnel = new bool[3];
        }

        [Serializable]public class Shot
        {
            //type: 0 - shoot, 1 - matrixShoot, 2 - toxic
            public int type;
            public int target;
            public int targetLeft;
            public int targetRight;
            public int from;
            public bool mafia;
            public int reportNumber;
            public bool first;
            public bool sniper;
            public int bulletNumber;
            public string textInfo2RTB;
        }

        [Serializable]public class gameState
        {
            public Cards[] cards;
            public Tunnel[] tunnels;
            public string[] report;
            public int numberOfTunnels;
            public int numberOfAlivePlayers;
            public Player[] players;
            public int nightPhase;
            public bool isNight;
            public int numOfNight = 0;
            public int numOfAllCards = 0;
            public int playersNamesSet = 0;
            public bool evenNight;
            public int clickedPlayer;
            public bool wantsUse;
            public bool canClickPictureBox;
            public bool matrix;
            public bool grabarz;
            public int matrixBullets = 0;
            public int mafiansAim;
            public int mafiaShoots;
            public Bullet bullet = new Bullet();
            public int nextMrakoszlap;
            public int pijawicaMrakoszlaps = 0;
            public int grabarzMrakoszlaps = 0;
            public int nextZwierciadlo;
            public int nextImunita;
            public int nextKewlar;
            public int nextProwazochodec;
            public List<int> sklenarMirrors = new List<int>();
            public List<int> wakedPlayers = new List<int>();
            public int votedShot;
            public int addRemoveCard = 0;
            public int selectedPlayer;
            public int graczKíeryKliko = 0;
            public int tunel1, tunel2;
            public int zachroniony, ofiara;
            public bool duchBoboExhibowolPoUmrziciu;
            public int posunyciDoktora;
            public bool kuskonaZyskolMraka;
            public bool gandalfZyskolMraka;
            public List<Shot> shots = new List<Shot>();
            public string InfoRTBText;
            public string Info2RTBText;
            public bool yesButtonEnabled;
            public bool yesButtonVisible;
            public bool noButtonEnabled;
            public bool noButtonVisible;
            public bool votedButtonEnabled;
            public bool votedButtonVisible;
            public bool shotButtonEnabled;
            public bool shotButtonVisible;
            public string shotButtonText;
            public bool buttonStartNightEnabled;
            public bool buttonStartNightVisible;
            public string buttonStartNightText;
            public bool addCardButtonEnabled;
            public bool addCardButtonVisible;
            public bool removeCardButtonEnabled;
            public bool removeCardButtonVisible;
            public bool addCardComboboxEnabled;
            public bool addCardComboboxVisible;
            public bool removeCardComboboxEnabled;
            public bool removeCardComboboxVisible;
            public bool undoButtonEnabled;
            public bool undoButtonVisible;
            public bool bombButtonEnabled;
            public bool bombButtonVisible;
            public string PlayersCardsRichTextBoxText;
            public int starterThread;
            public bool speedLabelVisible;
            public bool speedTrackBarEnabled;
            public bool speedTrackBarVisible;
            public bool flyingCheckBoxEnabled;
            public bool flyingCheckBoxVisible;
        }
        
        [Serializable]public class Card
        {
            public string name;
            public int player;
            public int uses;
            public bool inGame;

            public Card(string name, int player, int uses, bool inGame)
            {
                this.name = name;
                this.player = player;
                this.uses = uses;
                this.inGame = inGame;
            }
        }

        [Serializable]public struct Cards
        {
            public List<Card> cards;
            public int numInGame;
        }

        [Serializable]public struct Tunnel
        {
            public int from;
            public int to;
            public int numOfTunnel;
        }

        [Serializable]public class vector2D
        {
            public float x;
            public float y;

            public vector2D(float x, float y)
            {
                this.x = x;
                this.y = y;
            }

            public vector2D()
            {

            }

            public vector2D(int x, int y)
            {
                this.x = (float)x;
                this.y = (float)y;
            }

            public void add(vector2D v)
            {
                this.x += v.x;
                this.y += v.y;
            }

            public float mag()
            {
                return (float)Math.Sqrt(x * x + y * y);
            }

            public vector2D normalize()
            {
                float m = mag();
                if (m != 0 && m != 1)
                {
                    div(m);
                }
                return this;
            }

            public vector2D div(float n)
            {
                x /= n;
                y /= n;
                return this;
            }

            public vector2D setMag(float len)
            {
                normalize();
                mult(len);
                return this;
            }

            public vector2D mult(float n)
            {
                x *= n;
                y *= n;
                return this;
            }
        }

        // global variables
        //to ensure that bullets are fired on the end of the night
        public List<Shot> shots = new List<Shot>();
        Cards[] cards;
        Random rnd = new Random();
        Bitmap bmp;
        Graphics g;
        int width = 770;
        int height = 500;
        const int circleDiameter = 38;
        int[,] pictureBoxArray;
        Tunnel[] tunnels = new Tunnel[3];
        int numberOfTunnels;
        int numberOfPlayers;
        int numberOfAlivePlayers;
        Player[] players;
        int startPhase;
        int nightPhase;
        bool isNight = true;
        int numOfNight = 0;
        int numOfAllCards = 0;
        int playersNamesSet = 0;
        bool evenNight = false;
        const string endl = "\n";
        int clickedPlayer;
        bool wantsUse;
        bool canClickPictureBox = false;
        bool matrix = false;
        bool grabarz = false;
        int matrixBullets = 0;
        int mafiansAim;
        //numOfPlayers - hasnt aimed, -1 -> bad aim, no shooting
        int mafiaShoots;
        Bullet bullet = new Bullet();
        Thread thread2;
        int nextMrakoszlap;
        int pijawicaMrakoszlaps = 0;
        int grabarzMrakoszlaps = 0;
        int nextZwierciadlo;
        int nextImunita;
        int nextKewlar;
        int nextProwazochodec;
        List<int> sklenarMirrors = new List<int>();
        List<int> wakedPlayers = new List<int>();
        //0 - voted, 1 - shot
        int votedShot;
        //0 - nothing, 1 - add, 2 - remove
        int addRemoveCard = 0;
        //0 - night, 1 - day
        int starterThread = 0;
        bool endNight = false;
        bool undo = false;
        //used only in deleting cards from player
        int selectedPlayer;
        //zmjynio se na numer card gracza kiery w nocy pouzyl funkcje a musi na kogosi kliknoc
        int graczKíeryKliko = 0;
        int tunel1, tunel2;
        int zachroniony, ofiara;
        bool duchBoboExhibowolPoUmrziciu = false;
        // nahodne czislo, kiere urczuje, kiedy doktor beje mog lyczyc som siebie, aby nie szlo poznac, ze to nima mafian
        int posunyciDoktora;
        bool kuskonaZyskolMraka = false;
        bool gandalfZyskolMraka = false;
        string dateAndTimeWhenProgramStarted;
        static AutoResetEvent waitForClickYesNo = new AutoResetEvent(false);
        bool waitForClickYesNoset = false;
        static AutoResetEvent waitForClickPB = new AutoResetEvent(false);
        bool waitForClickPBset = false;
        List<gameState> gameStates = new List<gameState>();
        enum cardTypeNumber
        {
            mrakoszlap,
            imunita,
            prowazochodec,
            neprustrzelnoWesta,
            matrix,
            jailer,
            mag,
            slina,
            pijavica,
            piosek,
            kobra,
            magnet,
            fusekla,
            duchBobo,
            mafian,
            szilenyStrzelec,
            sniper,
            zwierciadlo,
            terorista,
            astronom,
            meciar,
            kovac,
            alCapone,
            gandalf,
            kusKona,
            ateista,
            anarchista,
            sklenar,
            masowyWrah,
            soudce,
            slepyKat,
            komunista,
            luneta,
            grabarz,
            panCzasu,
            doktor,
            jozinZBazin
        }
        /* 0 - budzi se
         * 1 - zakozane mowic mo
         * 2 - posliniony byl
         * 3 - pioskym dostol/niedostol
         * 4 - pocisk od mafianow
         * 5 - pocisk od szil 1
         * 6 - pocisk od szil 2
         * 7 - pocisk od snipera
         * 8 - kiela kulek matrix chycil
         * 9 -> 20 - kulki od matrixa
         * 21 - fusekla
         * 22 - pijavica zyskala mrakoszlapy
         * 23 - kobra zyskala mrakoszlapa a zjadla pijawice
         * 24 - grabarz zyskol mrakoszlapa
         * 25 - szklorz wyrobil zwierciadlo
         * 26 - gandalf zyskol mrakoszlapa
         * 27 - kuskona zyskol mrakoszlapa
         * 28 - zakozane glosowac mo
        */
        const int numberOfReports = 29;
        string[] report = new string[numberOfReports];
        Image slinaImage = Image.FromFile(".\\icons\\slina.png");
        Image piosekImage = Image.FromFile(".\\icons\\piosek.png");
        Image magnetImage = Image.FromFile(".\\icons\\magnet.png");
        Image matrixImage = Image.FromFile(".\\icons\\matrix.jpg");
        Image meciarImage = Image.FromFile(".\\icons\\dwa.png");
        Image kovacImage = Image.FromFile(".\\icons\\minus1.png");
        Image masovyVrahImage = Image.FromFile(".\\icons\\masovyVrah.png");
        Image soudceImage = Image.FromFile(".\\icons\\soudce.png");
        Image ofiaraImage = Image.FromFile(".\\icons\\ofiara.png");
        Image zachronionyImage = Image.FromFile(".\\icons\\zachroniony.png");
        int lang;
        Font font = new Font(FontFamily.GenericMonospace, 10, FontStyle.Bold);
        int[] amountOfSpecificCardsListBox = new int[numOfSpecificCards];
        int numOfAllCardsListBox = 0;
        readonly string[,] text =
        {
            /*  0 */{"Nazwa", "Jméno" },
            /*  1 */{"-go gracza", "-ho hráče" },
            /*  2 */{"Dla poprawnego rozdania kart musisz usunąć kilka kart. Karty usuwa się podwójnym przyciskiem myszki. Ilość kart, które jeszcze trzeba usunąć: ",
                     "Pro správné rozdání karet je třeba některé karty odstranit. Karty se odstraňuje dvojklikem. Počet karet, které je třeba ještě odstranit: " },
            /*  3 */{"Mafia wygrała.", "Vyhrála mafie." },
            /*  4 */{"Niewinni obywatele wygrali.", "Vyhráli poctiví občané." },
            /*  5 */{"Rozpocznij dzień", "Začni den" },
            /*  6 */{"Zaczyno noc i miasteczko idzie spać...", "Začíná noc a městečko jde spát..." },
            /*  7 */{"Gdo se nimo obudzić?", "Kdo se nemá probudit?" },
            /*  8 */{"Gracz", "Hráč" },
            /*  9 */{"se tej nocy nie obudzi.", "se této noci neprobudí." },
            /* 10 */{"Budzi se ", "Budí se "},
            /* 11 */{ ", chce użyć swojóm funkcje?", ", chce použít svoji schopnost?"},
            /* 12 */{"Grabarz użył swojóm funkcje.", "Hrobař použil svoji schopnost." },
            /* 13 */{"Následuj bílého králíka.", "Následuj bílého králíka." },
            /* 14 */{"Z kierego gracza zrobić tunel?", "Z kterého hráče udělat tunel?" },
            /* 15 */{"Na kierego gracza zrobić tunel?", "Na kterého hráče udělat tunel?" },
            /* 16 */{"Kierego gracza poślinić?", "Kterého hráče poslintat?" },
            /* 17 */{"Poślinióny był ", "Poslintaný byl " },
            /* 18 */{"Na kierego gracza se przissać?", "Na kterého hráče se přisát?" },
            /* 19 */{"Pijavica se przissała na gracza ", "Pijavice se přisála na hráče " },
            /* 20 */{"Kierego gracza posypać pioskym?", "Kterého hráče posypat pískem?" },
            /* 21 */{"Gracza posypanego pioskym ochróniła gas maska.", "Hráče posypaného pískem zachránila plynová maska." },
            /* 22 */{" nimoże być posypany pioskym, bo mo gas maske.", " nemůže být posypaný pískem, protože má plynovou masku." },
            /* 23 */{"Pioskym dostoł ", "Pískem dostal " },
            /* 24 */{"Kierego gracza ugryzie kobra?", "Kterého hráče kousne kobra?" },
            /* 25 */{"Ugryziony był ", "Kousnutý byl " },
            /* 26 */{"Kierego gracza zmagnetyzować?", "Kterého hráče zmagnetizovat?" },
            /* 27 */{"Zmagnetyzowany był ", "Zmagnetizovaný byl " },
            /* 28 */{"Kierego gracza exhibnóć?", "Kterého hráče exhibnout?" },
            /* 29 */{"Exhibnóty był ", "Exhibnutý byl " },
            /* 30 */{"Zakozane mówić mo ", "Zákaz mluvit má " },
            /* 31 */{"Kierego gracza ulyczyć? (może sóm siebie)", "Kterého hráče vyléčit? (může sám sebe)" },
            /* 32 */{"Kierego gracza ulyczyć? (nimoże sóm siebie)", "Kterého hráče vyléčit? (nemůže sám sebe)" },
            /* 33 */{" nie bedzie ulyczóny, bo doktor je sóm proci mafianóm.", " nebude vyléčen, protože doktor je sám proti mafii." },
            /* 34 */{" bedzie ulyczóny.", " bude vyléčen." },
            /* 35 */{"Na kierego gracza chynyć błoto?", "Na kterého hráče hodit bláto?" },
            /* 36 */{" był pochlapany błotym.", " dostal blátem." },
            /* 37 */{"Chce ", "Chce " },
            /* 38 */{" użyć swojóm funkcje eszcze roz?", " použít svou schopnost ještě jednou?" },
            /* 39 */{"Kierego gracza zamierzić?", "Kterého hráče zaměřit?" },
            /* 40 */{"Mafian", "Mafián" },
            /* 41 */{" zamierził gracza ", " zaměřil hráče " },
            /* 42 */{", kiery je mafianym, je tej nocy we więzieniu, także mafia nie wystrzeli.", ", který je mafián, je tuto noc ve vězení, takže mafie nevystřelí." },
            /* 43 */{"Mafii se niepodarziło zamierzić.", "Mafii se nepodařilo zaměřit." },
            /* 44 */{" wystrzelił na gracza ", " vysřelil na hráče " },
            /* 45 */{"Na kierego gracza wystrzelić?", "Na kterého hráče vystřelit?" },
            /* 46 */{"Strzelec", "Střelec" },
            /* 47 */{"Sniper ", "Sniper " },
            /* 48 */{"Fusekla zaśmierdziała.", "Ponožka zasmrděla." },
            /* 49 */{"Kiery gracz bedzie mieć zakaz głosowanio?", "Který hráč bude mít zákaz hlasování?" },
            /* 50 */{" bedzie mieć zakaz głosowanio.", " bude mít zákaz hlasování." },
            /* 51 */{"Zakozane głosować mo ", "Zákaz hlasování má " },
            /* 52 */{"Kierego gracza zachrónić przed szibenicóm?", "Kterého hráče zachránit před šibenicí?" },
            /* 53 */{"Gdo mo zamiast niego skóńczyć na szibenicy?", "Kdo má místo něho skončit na šibenici?" },
            /* 54 */{" bedzie zachrónióny przed szibenicóm a zamiast niego na ni skóńczy ", " bude zachráněn před šibenicí a místo něho na ni skončí " },
            /* 55 */{"Informacja do lunety:", "Informace pro lunetu:" },
            /* 56 */{"Obudził se ", "Probudil se " },
            /* 57 */{"Dowiedziała se luneta gdo se obudził?", "Dozvěděla se luneta kdo se probudil?" },
            /* 58 */{" uż je mortwy.", " už je mrtvý." },
            /* 59 */{"Padnół strzał, ale pocisk lecioł na gracza, kiery uż był mortwy.", "Byla vystřelena kulka, ale letěla na hráče, který už byl mrtvý." },
            /* 60 */{"Ilość pocisków, kiere matrix przechwycił: ", "Počet kulek, které matrix chytil: " },
            /* 61 */{"W tej nocy matrix użył swojóm funkcje. Ilość pocisków, kiere przechwycił: ", "Tuto noc použil matrix svou schopnost. Počet kulek, které chytil: " },
            /* 62 */{"Matrix wystrzelił pocisk numer ", "Matrix vystřelil kulku číslo " },
            /* 63 */{"Matrix ", "Matrix " },
            /* 64 */{"Ilość pocisków, kiere matrixowi zbywajóm: ", "Počet kulek, které matrixovi zbývají: " },
            /* 65 */{"Kuskona", "Kuskoňa" },
            /* 66 */{" zyskoł mrakoszlapa ", " získal mrákošlapa " },
            /* 67 */{"Gandalf", "Gandalf" },
            /* 68 */{" zyskała mrakoszlapa ", " získala mrákošlapa " },
            /* 69 */{"Pijawica", "Pijavice" },
            /* 70 */{"Kobra", "Kobra" },
            /* 71 */{" a zniszczyła pijawice.", " a snědla pijavici." },
            /* 72 */{"Grabarz", "Hrobař" },
            /* 73 */{" zrobił zwierciadło ", " udělal zrcadlo " },
            /* 74 */{"Szklorz", "Sklénář" },
            /* 75 */{"Budzi se miasteczko Palermo.", "Budí se městečko Palermo." },
            /* 76 */{"Budzi se mafia, aby se mógła domówić jak bedzie działać.", "Budí se mafie, aby se mohla domluvit jak bude zabíjet." },
            /* 77 */{"Domówiła se mafia?", "Domluvila se mafie?" },
            /* 78 */{"Był strzelóny", "Byl postřelen" },
            /* 79 */{"Rozpocznij noc", "Začíná noc" },
            /* 80 */{"Padnół strzał", "Byla vystřelena kulka" },
            /* 81 */{" je matrix a chycił pocisk.", " je matrix a chytil kulku." },
            /* 82 */{" a matrix go chycił.", " a matrix ji chytil." },
            /* 83 */{"Pocisk był od gracza ", "Kulka byla od hráče " },
            /* 84 */{" przicióngnyty magnetym na gracza ", " přitažena megnetem na hráče " },
            /* 85 */{", był przicióngnyty magnetym", ", byla přitažena magnetem" },
            /* 86 */{" na gracza, kiery uż był mortwy.", " na hráče, který už byl mrtvý." },
            /* 87 */{", rozszczepił se na ", ", rozštěpila se na " },
            /* 88 */{" czynści", " částí" },
            /* 89 */{"Pocisk przelecioł tunelym ", "Kulka přeletěla tunelem " },
            /* 90 */{" z gracza ", " z hráče " },
            /* 91 */{" na gracza ", " na hráče " },
            /* 92 */{", przelecioł tunelym", ", přeletěla tunelem" },
            /* 93 */{" był poślinióny, tak se na nim pocisk ześlizgnół.", " byl poslintaný, tak na něm kulka uklouzla." },
            /* 94 */{" a ześlizgnół se na ślinie.", " a uklouzla na slině." },
            /* 95 */{" był poślinióny, ale też posypany pioskym.", " byl poslintaný, ale taky posypaný pískem." },
            /* 96 */{" je Al Capone.", " je Al Capone." },
            /* 97 */{" a deaktywowoł go Al Capone.", " a deaktivoval ji Al Capone." },
            /* 98 */{" był ulyczóny przez doktora.", " byl vyléčen doktorem." },
            /* 99 */{" a gracz był ulyczóny przez doktora.", " a hráč byl vyléčen doktorem." },
            /* 100 */{"Graczowi ", "Hráči " },
            /* 101 */{" se rozbiło zwierciadło ", " se rozbilo zrcadlo " },
            /* 102 */{" a pocisk se wrócił z powrotym na gracza ", " a kulka se vrátila zpět na hráče " },
            /* 103 */{", rozbiło se zwierciadło ", ", rozbilo se zrcadlo " },
            /* 104 */{", pocisk se wrócił z powrotym", ", kulka se vrátila zpět" },
            /* 105 */{", ale pocisk leci dali, bo je od snipera.", ", ale kulka letí dál, protože je od snipera." },
            /* 106 */{", ale pocisk leci dali", ", ale kulka letí dál" },
            /* 107 */{", ale pocisk leci dali, bo zwierciadło było pochlapane błotym.", ", ale kulka letí dál, protože zrcadlo bylo od bláta." },
            /* 108 */{" stracił kewlar ", " ztratil kevlar " },
            /* 109 */{" a umrził kewlar ", " a zemřel kevlar " },
            /* 110 */{" a umrził mrakoszlap ", " a zemřel mrákošlap " },
            /* 111 */{"W tej nocy fusekla użyła swojóm funkcje. ", "Tuto noc ponožka použila svoji schopnost. " },
            /* 112 */{"Jednego gracza zachróniła gas maska ", "Jednoho hráče ochránila plynová maska " },
            /* 113 */{" mo gas maske.", " má plynovou masku." },
            /* 114 */{"Jedyn gracz uż je mortwy ", "Jeden hráč už je mrtvý " },
            /* 115 */{"Jednego gracza ulyczył doktor ", "Jednoho hráče vyléčil doktor " },
            /* 116 */{" stracił mrakoszlapa ", " ztratil mrákošlapa " },
            /* 117 */{"Jednymu graczowi umrził mrakoszlap ", "Jednomu hráči zemřel mrákošlap " },
            /* 118 */{"Naszóm gre opuszczo ", "Naši hru opouští " },
            /* 119 */{"a drugigo gracza zachróniła gas maska.", "a druhého hráče ochránila plynová maska." },
            /* 120 */{"a drugi gracz uż je mortwy.", "a druhý hráč už je mrtvý." },
            /* 121 */{"a drugigo gracza ulyczył doktor.", "a druhého hráče vyléčil doktor." },
            /* 122 */{"a drugimu graczowi umrził mrakoszlap ", "a druhému hráči zemřel mrákošlap " },
            /* 123 */{"a naszóm gre opuszczo ", "a naši hru opouští " },
            /* 124 */{" stracił prowazochodca ", " ztratil provazochodce " },
            /* 125 */{"Umrził prowazochodec ", "Zemřel provazochodec " },
            /* 126 */{" stracił imunite ", " ztratil imunitu " },
            /* 127 */{"Umrziła imunita ", "Zemřela imunita " },
            /* 128 */{"Umrził mrakoszlap ", "Zemřel mrákošlap " },
            /* 129 */{"Umrził kewlar ", "Zemřel kevlar " },
            /* 130 */{" a naszóm gre opuszczo ", " a naši hru opouští " },
            /* 131 */{" umrził.", " zemřel." },
            /* 132 */{"SPI!!! ", "SPÍ!!! " },
            /* 133 */{" - je we więzieniu.", " - je ve vězení." },
            /* 134 */{" zyskoł neprustrzelnóm weste ", " získal kevlar " },
            /* 135 */{" zyskoł zwierciadło ", " získal zrcadlo " },
            /* 136 */{" zyskoł imunite ", " získal imunitu " },
            /* 137 */{" zyskoł prowazochodca ", " získal provazochodce " },
            /* 138 */{" stracił karte ", " ztratil kartu "},
            /* 139 */{"Między graczami ", "Mezi hráči " },
            /* 140 */{" już jest tunel.", " už je tunel." },
            /* 141 */{"Na gracza ", "Na hráče " },
            /* 142 */{" nie idzie zrobić tunel, bo je ateista.", " nejde udělat tunel, protože je ateista." },
            /* 143 */{"Tunel zrobióny z gracza ", "Tunel udělán z hráče " },
            /* 144 */{"Ilość graczy:", "Počet hráčů:" },
            /* 145 */{"Ja", "Ano" },
            /* 146 */{"Ni", "Ne" },
            /* 147 */{"Był przegłosowany", "Byl přehlasován" },
            /* 148 */{"Dodaj kartę", "Přidej kartu" },
            /* 149 */{"Odbierz kartę", "Odeber kartu" },
            /* 150 */{"Bómba", "Bomba" },
            /* 151 */{". ułamek przelecioł tunelym", ". část přeletěla tunelem" },
            /* 152 */{"Czas pocisków (ms): ", "Čas kulek (ms): " },
            /* 153 */{"Szybkość pocisków (px/ms): ", "Rychlost kulek (px/ms): " },
            /* 154 */{"Lotajónce kulki: ", "Létající kulky: " },
            /* 155 */{"Karta ", "Karta " },
            /* 156 */{" nimoże być graczowi", " nemůže být hráči" },
            /* 157 */{" odebrano", " odebrána" },
            /* 158 */{"Chcesz, aby komputer rozdol karty?", "Chceš, aby počítač rozdělil karty?" },
        };
        
        // functions for initializing stuff

        private void Form1_Load(object sender, EventArgs e)
        {
            startPhase0();
        }

        public void startPhase0()
        {
            try
            {
                dateAndTimeWhenProgramStarted = DateTime.Today.ToString("yyyy-MM-dd") + "_" + DateTime.Now.ToString("HH-mm-ss");
                thread2 = new Thread(starter);
                thread2.IsBackground = true;
                startPhase = 0;
                pictureBox1.Enabled = false;
                pictureBox1.Visible = false;
                InfoRTB.Enabled = false;
                InfoRTB.Visible = false;
                Info2RTB.Enabled = false;
                Info2RTB.Visible = false;
                textBoxPlayerName.Enabled = false;
                textBoxPlayerName.Visible = false;
                CardsListBox.Enabled = false;
                CardsListBox.Visible = false;
                yesButton.Enabled = false;
                yesButton.Visible = false;
                noButton.Enabled = false;
                noButton.Visible = false;
                InfoLabel.Enabled = false;
                InfoLabel.Visible = false;
                votedButton.Enabled = false;
                votedButton.Visible = false;
                shotButton.Enabled = false;
                shotButton.Visible = false;
                buttonStartNight.Enabled = false;
                buttonStartNight.Visible = false;
                addCardButton.Enabled = false;
                addCardButton.Visible = false;
                removeCardButton.Enabled = false;
                removeCardButton.Visible = false;
                addCardCombobox.Enabled = false;
                addCardCombobox.Visible = false;
                removeCardCombobox.Enabled = false;
                removeCardCombobox.Visible = false;
                undoButton.Enabled = false;
                undoButton.Visible = false;
                numOfPlayersNumericUpDown.Enabled = false;
                numOfPlayersNumericUpDown.Visible = false;
                bombButton.Enabled = false;
                bombButton.Visible = false;
                speedLabel.Visible = false;
                speedTrackBar.Enabled = false;
                speedTrackBar.Visible = false;
                flyingCheckBox.Enabled = false;
                flyingCheckBox.Visible = false;
                labelStartPhase.Text = "Please select a language:";
                comboBoxLanguage.SelectedIndex = 0;
                AcceptButton = buttonStartPhase;
                initializeCards();
                numOfPlayersNumericUpDown.Focus();
                posunyciDoktora = rnd.Next(2);
                for (int i = 0; i < numberOfReports; i++)
                {
                    report[i] = "";
                }
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    amountOfSpecificCardsListBox[i] = amountOfSpecificCards[i];
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void initializeCards()
        {
            try
            {
                cards = new Cards[numOfSpecificCards];
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    cards[i].cards = new List<Card>();
                    for(int j = 0; j < amountOfSpecificCards[i]; j++)
                    {
                        cards[i].cards.Add(new Card(cardNames[i, lang], -1, 1, true));
                    }
                }
                
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    cards[i].numInGame = amountOfSpecificCards[i];
                };
                cards[(int)cardTypeNumber.slepyKat].cards[0].uses = 2;
                cards[(int)cardTypeNumber.jozinZBazin].cards[0].uses = 3;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void buttonStartPhase_Click(object sender, EventArgs e)
        {
            try
            {
                // setting language
                if(startPhase == 0)
                {
                    lang = comboBoxLanguage.SelectedIndex;
                    comboBoxLanguage.Visible = false;
                    comboBoxLanguage.Enabled = false;
                    numOfPlayersNumericUpDown.Enabled = true;
                    numOfPlayersNumericUpDown.Visible = true;
                    labelStartPhase.Text = text[144, lang];
                    yesButton.Text = text[145, lang];
                    noButton.Text = text[146, lang];
                    votedButton.Text = text[147, lang];
                    addCardButton.Text = text[148, lang];
                    removeCardButton.Text = text[149, lang];
                    bombButton.Text = text[150, lang];
                    speedLabel.Text = text[152, lang] + speedTrackBar.Value;
                    flyingCheckBox.Text = text[154, lang];
                    startPhase = 1;
                }
                // setting num of players
                else if (startPhase == 1)
                {
                    
                    numberOfPlayers = (int)numOfPlayersNumericUpDown.Value;
                    numberOfAlivePlayers = numberOfPlayers;
                    players = new Player[numberOfPlayers];
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        players[i] = new Player();
                        players[i].tunnels = new List<Tunnel>();
                    }
                    numOfPlayersNumericUpDown.Enabled = false;
                    numOfPlayersNumericUpDown.Visible = false;
                    labelStartPhase.Text = text[0, lang] + ' ' + (playersNamesSet + 1) + text[1, lang] + ": ";
                    textBoxPlayerName.Enabled = true;
                    textBoxPlayerName.Visible = true;
                    textBoxPlayerName.Focus();
                    startPhase = 2;
                }
                // setting players names
                else if (startPhase == 2)
                {
                    if (textBoxPlayerName.Text != "")
                    {
                        players[playersNamesSet].name = textBoxPlayerName.Text;
                        playersNamesSet++;
                        textBoxPlayerName.Text = "";
                        textBoxPlayerName.Focus();
                        labelStartPhase.Text = text[0, lang] + ' ' + (playersNamesSet + 1) + text[1, lang] + ": ";
                        if (playersNamesSet == numberOfPlayers)
                        {
                            labelStartPhase.Text = "";
                            textBoxPlayerName.Enabled = false;
                            textBoxPlayerName.Visible = false;
                            startPhase = 3;
                            buttonStartPhase.PerformClick();
                        }
                    }
                }
                // deleting cards
                else if (startPhase == 3)
                {
                    setNumberOfAllCards();
                    setNumberOfAllCardsListBox();
                    if (numOfAllCards % numberOfPlayers != 0)
                    {
                        buttonStartPhase.Enabled = false;
                        buttonStartPhase.Visible = false;
                        CardsListBox.Enabled = true;
                        CardsListBox.Visible = true;
                        drawCardsListBox();
                        labelStartPhase.MaximumSize = new Size(250, 0);
                        labelStartPhase.AutoSize = true;
                        labelStartPhase.Text = text[2, lang] + numOfAllCards % numberOfPlayers + ".";
                    }
                    else
                    {
                        for (int i = 0; i < numberOfPlayers; i++)
                        {
                            players[i].cardNumbers = new List<int>();
                            players[i].cardTypes = new List<int>();
                        }
                        CardsListBox.Visible = false;
                        pictureBox1.Enabled = true;
                        pictureBox1.Visible = true;
                        initializePictureBox();
                        initPlayers();
                        drawPlayers();
                        drawPlayersCardsRTB();
                        labelStartPhase.Visible = false;
                        labelStartPhase.Enabled = false;
                        buttonStartPhase.Width = 0;
                        yesButton.Enabled = true;
                        yesButton.Visible = true;
                        noButton.Enabled = true;
                        noButton.Visible = true;
                        InfoLabel.Enabled = true;
                        InfoLabel.Visible = true;
                        InfoLabel.Text = text[158, lang];
                        AcceptButton = yesButton;
                    }
                }
                // giving cards
                else if(startPhase == 4)
                {
                    yesButton.Enabled = false;
                    yesButton.Visible = false;
                    noButton.Enabled =  false;
                    noButton.Visible =  false;
                    InfoLabel.Enabled = false;
                    InfoLabel.Visible = false;
                    CardsListBox.Enabled = true;
                    CardsListBox.Visible = true;
                    CardsListBox.Left = bombButton.Location.X;
                    CardsListBox.Top = bombButton.Location.Y;
                    drawPlayersCardsRTB();
                    canClickPictureBox = true;
                }
                // initializing a couple of things and starting the game
                else if (startPhase == 5)
                {
                    nextMrakoszlap = amountOfSpecificCards[(int)cardTypeNumber.mrakoszlap];
                    nextZwierciadlo = amountOfSpecificCards[(int)cardTypeNumber.zwierciadlo];
                    nextImunita = amountOfSpecificCards[(int)cardTypeNumber.imunita];
                    nextKewlar = amountOfSpecificCards[(int)cardTypeNumber.neprustrzelnoWesta];
                    nextProwazochodec = amountOfSpecificCards[(int)cardTypeNumber.prowazochodec];
                    CardsListBox.Visible = false;
                    CardsListBox.Enabled = false;
                    AcceptButton = buttonStartPhase;
                    buttonStartPhase.Enabled = false;
                    startPhase = -1;
                    nightPhase = 0;
                    InfoLabel.Enabled = true;
                    InfoLabel.Visible = true;
                    InfoRTB.Enabled = true;
                    InfoRTB.Visible = true;
                    Info2RTB.Enabled = true;
                    Info2RTB.Visible = true;
                    yesButton.Enabled = true;
                    yesButton.Visible = true;
                    noButton.Enabled = true;
                    noButton.Visible = true;
                    shotButton.Enabled = true;
                    shotButton.Visible = true;
                    speedLabel.Visible = true;
                    speedTrackBar.Enabled = true;
                    speedTrackBar.Visible = true;
                    flyingCheckBox.Enabled = true;
                    flyingCheckBox.Visible = true;
                    thread2.Start();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void setNumberOfAllCards()
        {
            try
            {
                numOfAllCards = 0;
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    numOfAllCards += cards[i].numInGame;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void setNumberOfAllCardsListBox()
        {
            try
            {
                numOfAllCardsListBox = 0;
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    numOfAllCardsListBox += amountOfSpecificCardsListBox[i];
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawCardsListBox()
        {
            try
            {
                CardsListBox.Items.Clear();
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    CardsListBox.Items.Add(cardNames[i, lang] + " (" + amountOfSpecificCardsListBox[i] + ")");
                }
                CardsListBox.Height = CardsListBox.PreferredHeight;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void CardsListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                int cardType = Convert.ToInt32(Math.Floor(e.Y / (CardsListBox.Height / (double)numOfSpecificCards)));
                if (amountOfSpecificCards[cardType] > 0 && numOfAllCards % numberOfPlayers != 0)
                {
                    cards[cardType].cards[cards[cardType].numInGame - 1].inGame = false;
                    amountOfSpecificCards[cardType]--;
                    amountOfSpecificCardsListBox[cardType]--;
                    cards[cardType].numInGame--;
                    numOfAllCards--;
                    numOfAllCardsListBox--;
                    if (numOfAllCards % numberOfPlayers == 0)
                    {
                        drawCardsListBox();
                        CardsListBox.Enabled = false;
                        buttonStartPhase.Enabled = true;
                        buttonStartPhase.Visible = true;
                        startPhase = 3;
                        buttonStartPhase.PerformClick();
                    }
                    else
                    {
                        labelStartPhase.Text = text[2, lang] + numOfAllCards % numberOfPlayers + ".";
                        drawCardsListBox();
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void giveCards()
        {
            try
            {
                int[] cardTypes = new int[numOfAllCards];
                int[] cardNumbers = new int[numOfAllCards];
                int iter = 0;
                for (int cardType = 0; cardType < numOfSpecificCards; cardType++)
                {
                    for (int cardNumber = 0; cardNumber < cards[cardType].numInGame; cardNumber++)
                    {
                        cardTypes[iter] = cardType;
                        cardNumbers[iter] = cardNumber;
                        iter++;
                    }
                }
                bool restart;
                do
                {
                    int zmiana1, zmiana2, pomoc;
                    for (int i = 0; i < 50000; i++)
                    {
                        zmiana1 = rnd.Next(numOfAllCards);
                        zmiana2 = rnd.Next(numOfAllCards);
                        if (zmiana1 != zmiana2)
                        {
                            pomoc = cardTypes[zmiana1];
                            cardTypes[zmiana1] = cardTypes[zmiana2];
                            cardTypes[zmiana2] = pomoc;
                            pomoc = cardNumbers[zmiana1];
                            cardNumbers[zmiana1] = cardNumbers[zmiana2];
                            cardNumbers[zmiana2] = pomoc;
                        }
                    }
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        players[i].cardNumbers.Clear();
                        players[i].cardTypes.Clear();
                        for (int j = 0; j < numOfAllCards / numberOfPlayers; j++)
                        {
                            int card = i * numOfAllCards / numberOfPlayers + j;
                            players[i].cardTypes.Add(cardTypes[card]);
                            players[i].cardNumbers.Add(cardNumbers[card]);
                            cards[cardTypes[card]].cards[cardNumbers[card]].player = i;
                        }
                    }
                    restart = false;
                    for (int i = 0; i < numberOfPlayers && !restart; i++)
                    {
                        bool hasMag2 = false;
                        bool hasSilenyStrzelec1 = false;
                        bool hasSilenyStrzelec2 = false;
                        bool hasAlCapone = false;
                        bool hasKusKona = false;
                        bool hasGandalf = false;
                        bool hasPijawica = false;
                        bool hasKobra = false;
                        bool hasAteista = false;
                        bool hasMatrix = false;
                        bool hasGrabarz = false;
                        bool hasDoktor = false;
                        bool hasLuneta = false;
                        int numOfMrakoszlaps = 0;
                        int numOfProwazochodec = 0;
                        int mafia = 0;
                        for (int j = 0; j < numOfAllCards / numberOfPlayers; j++)
                        {
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.mag && players[i].cardNumbers[j] == 1) { hasMag2 = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.szilenyStrzelec && players[i].cardNumbers[j] == 0) { hasSilenyStrzelec1 = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.szilenyStrzelec && players[i].cardNumbers[j] == 1) { hasSilenyStrzelec2 = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.alCapone) { hasAlCapone = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.kusKona) { hasKusKona = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.gandalf) { hasGandalf = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.pijavica) { hasPijawica = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.kobra) { hasKobra = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.mafian) { mafia++; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.ateista) { hasAteista = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.matrix) { hasMatrix = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.luneta) { hasLuneta = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.grabarz) { hasGrabarz = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.doktor) { hasDoktor = true; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.mrakoszlap) { numOfMrakoszlaps++; }
                            if (players[i].cardTypes[j] == (int)cardTypeNumber.prowazochodec) { numOfProwazochodec++; }
                        }
                        if (numOfMrakoszlaps > 1 && (hasGrabarz || hasPijawica || hasAlCapone || hasGandalf || hasKusKona))
                        {
                            restart = true;
                        }
                        if (numOfMrakoszlaps > 2 || numOfProwazochodec > 2)
                        {
                            restart = true;
                        }
                        if (mafia > 1)
                        {
                            restart = true;
                        }
                        if (mafia == 1 && (hasLuneta || hasDoktor))
                        {
                            restart = true;
                        }
                        if (hasSilenyStrzelec1 && hasSilenyStrzelec2)
                        {
                            restart = true;
                        }
                        if ((hasMag2 && hasSilenyStrzelec2) || (hasMag2 && hasAlCapone) || (hasSilenyStrzelec2 && hasAlCapone))
                        {
                            restart = true;
                        }
                        if (hasKusKona && hasGandalf)
                        {
                            restart = true;
                        }
                        if (hasPijawica && hasKobra)
                        {
                            restart = true;
                        }
                        if (hasAteista && hasMatrix)
                        {
                            restart = true;
                        }
                    }
                }
                while (restart);

                //if player with Jozin z Bazin also has mafia, sileny strelec or sniper, he has only 2 blotos
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    bool hasSilenyStrzelec1 = false;
                    bool hasSilenyStrzelec2 = false;
                    bool hasJozinZBazin = false;
                    bool hasSniper = false;
                    int mafia = 0;
                    for (int j = 0; j < numOfAllCards / numberOfPlayers; j++)
                    {
                        if (players[i].cardTypes[j] == (int)cardTypeNumber.szilenyStrzelec && players[i].cardNumbers[j] == 0) { hasSilenyStrzelec1 = true; }
                        if (players[i].cardTypes[j] == (int)cardTypeNumber.szilenyStrzelec && players[i].cardNumbers[j] == 1) { hasSilenyStrzelec2 = true; }
                        if (players[i].cardTypes[j] == (int)cardTypeNumber.mafian) { mafia++; }
                        if (players[i].cardTypes[j] == (int)cardTypeNumber.jozinZBazin) { hasJozinZBazin = true; }
                        if (players[i].cardTypes[j] == (int)cardTypeNumber.sniper) { hasSniper = true; }
                    }
                    if (hasJozinZBazin && (hasSilenyStrzelec1 || hasSilenyStrzelec2 || mafia > 0 || hasSniper))
                    {
                        players[i].numberOfBloto = 2;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawPlayersCardsRTB()
        {
            try
            {
                //sorting of players cards alphabetically and by number
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    for (int j = players[i].cardNumbers.Count - 1; j >= 0; j--)
                    {
                        bool changed = false;
                        for (int k = 0; k < j; k++)
                        {
                            int num1 = players[i].cardNumbers[k];
                            int num2 = players[i].cardNumbers[k + 1];
                            int compare = String.Compare(nameOfCard(players[i].cardTypes[k], players[i].cardNumbers[k]), nameOfCard(players[i].cardTypes[k + 1], players[i].cardNumbers[k + 1]));
                            if ((compare > 0 && players[i].cardTypes[k] != players[i].cardTypes[k + 1]) || (compare < 0 && players[i].cardTypes[k] == players[i].cardTypes[k + 1] && num1 > num2))
                            {
                                changed = true;
                                int pom = players[i].cardTypes[k];
                                players[i].cardTypes[k] = players[i].cardTypes[k + 1];
                                players[i].cardTypes[k + 1] = pom;
                                pom = players[i].cardNumbers[k];
                                players[i].cardNumbers[k] = players[i].cardNumbers[k + 1];
                                players[i].cardNumbers[k + 1] = pom;
                            }
                        }
                        if (!changed) { break; }
                    }
                }
                string newContent = "";

                int[] spaces = new int[numberOfPlayers];
                int maxCards = 0;
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    int max = 0;
                    if (players[i].alive)
                    {
                        for (int j = 0; j < players[i].cardNumbers.Count; j++)
                        {
                            string add = nameOfCard(players[i].cardTypes[j], players[i].cardNumbers[j]);
                            if (add.Length > max)
                            {
                                max = add.Length;
                            }
                        }
                    }
                    if (players[i].cardNumbers.Count > maxCards)
                    {
                        maxCards = players[i].cardNumbers.Count;
                    }
                    spaces[i] = max;
                }
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    if (players[i].alive)
                    {
                        newContent += players[i].name;
                        for (int k = 0; k < spaces[i] + 3 - players[i].name.Length; k++)
                        {
                            newContent += " ";
                        }
                    }
                }
                newContent += "\n";
                for (int j = 0; j < maxCards; j++)
                {
                    newContent += " ";
                    for (int i = 0; i < numberOfPlayers; i++)
                    {
                        if (players[i].alive)
                        {
                            if (j < players[i].cardNumbers.Count)
                            {
                                string add = nameOfCard(players[i].cardTypes[j], players[i].cardNumbers[j]);
                                newContent += add;
                                for (int k = 0; k < spaces[i] + 3 - add.Length; k++)
                                {
                                    newContent += " ";
                                }
                            }
                            else
                            {
                                for (int k = 0; k < spaces[i] + 3; k++)
                                {
                                    newContent += " ";
                                }
                            }
                        }
                    }
                    newContent += "\n";
                }
                this.Invoke((MethodInvoker)delegate { PlayersCardsRichTextBox.Text = newContent; });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void initializePictureBox()
        {
            try
            {
                pictureBox1.Width = width;
                pictureBox1.Height = height;
                bmp = new Bitmap(width, height);
                g = Graphics.FromImage(bmp);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.Clear(Color.White);
                pictureBox1.Image = bmp;
                pictureBoxArray = new int[width, height];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        pictureBoxArray[i, j] = numberOfPlayers;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void initPlayers()
        {
            try
            {
                int distX = (int)(width / 2.55);
                int distY = (int)(height / 2.7);
                int player = 0;
                for (double a = Math.PI / 2; a > -3 * Math.PI / 2; a -= Math.PI * (2 / (double)numberOfPlayers))
                {
                    if (player < numberOfPlayers)
                    {
                        int x = width / 2 + (int)(Math.Cos(a) * distX);
                        int y = height / 2 + (int)(-Math.Sin(a) * distY);
                        players[player].position.x = x;
                        players[player].position.y = y;
                        for (int i = x - circleDiameter; i < x + circleDiameter; i++)
                        {
                            for (int j = y - circleDiameter; j < y + circleDiameter; j++)
                            {
                                if (i >= 0 && i < width && j >= 0 && j < height)
                                {
                                    if (distance(x, y, i, j) >= circleDiameter - 1 && distance(x, y, i, j) <= circleDiameter)
                                    {
                                        pictureBoxArray[i, j] = player;
                                    }
                                    else if (distance(x, y, i, j) < circleDiameter - 1)
                                    {
                                        pictureBoxArray[i, j] = player;
                                    }
                                }
                            }
                        }
                        player++;
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        // functions for the game and stuff

        public void starter()
        {
            try
            {
                while (numberOfAlivePlayers > cards[(int)cardTypeNumber.mafian].numInGame && cards[(int)cardTypeNumber.mafian].numInGame > 0)
                {
                    saveGameState();
                    if (starterThread == 0)
                    {
                        int tmp = night();
                        if (!undo)
                        {
                            starterThread = tmp;
                        }
                    }
                    else
                    {
                        int tmp = day();
                        if (!undo)
                        {
                            starterThread = tmp;
                        }
                    }
                }
                string raport = "";
                for (int i = 0; i < numberOfReports; i++)
                {
                    if (report[i] != "")
                    {
                        raport += report[i] + endl;
                        report[i] = "";
                    }
                }

                this.Invoke((MethodInvoker)delegate
                {
                    if (raport != "")
                    {
                        InfoRTB.Text += endl + raport;
                    }

                    yesButton.Enabled = false;
                    yesButton.Visible = false;
                    noButton.Enabled = false;
                    noButton.Visible = false;
                    votedButton.Enabled = false;
                    votedButton.Visible = false;
                    shotButton.Enabled = false;
                    shotButton.Visible = false;
                    buttonStartNight.Enabled = false;
                    buttonStartNight.Visible = false;
                    undoButton.Enabled = false;
                    undoButton.Visible = false;
                    bombButton.Enabled = false;
                    bombButton.Visible = false;
                    addCardButton.Enabled = false;
                    addCardButton.Visible = false;
                    removeCardButton.Enabled = false;
                    removeCardButton.Visible = false;
                    if (cards[(int)cardTypeNumber.mafian].numInGame > 0)
                    {
                        InfoLabel.Text = text[3, lang];
                    }
                    else
                    {
                        InfoLabel.Text = text[4, lang];
                    }
                    InfoLabel.Focus();
                });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }
        
        public void initNight()
        {
            try
            {
                isNight = true;
                numOfNight++;
                matrix = false;
                grabarz = false;
                kuskonaZyskolMraka = false;
                gandalfZyskolMraka = false;
                for (int i = 0; i < 3; i++)
                {
                    tunnels[i].from = -1;
                }
                string raport = "";
                for (int i = 0; i < numberOfReports; i++)
                {
                    if (report[i] != "")
                    {
                        raport = report[i] + endl;
                    }
                    report[i] = "";
                }
                if(raport != "")
                {
                    raport += endl;
                    drawPlayersCardsRTB();
                }
                resetBullet();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    players[i].wake = true;
                    players[i].tunnelFrom = false;
                    players[i].hasPiosek = false;
                    players[i].hasSlina = false;
                    players[i].hasPijavica = false;
                    players[i].hasMagnet = false;
                    players[i].hasKobra = false;
                    players[i].hasExhib = false;
                    players[i].hasZakazGlosowanio = false;
                    players[i].hasDoktor = false;
                    players[i].ofiaraKata = false;
                    players[i].zachronionyKatym = false;
                    players[i].tunnelsFrom = 0;
                    players[i].numberOfBloto = 0;
                    players[i].tunnels.Clear();
                }
                numberOfTunnels = 0;
                mafiaShoots = numberOfPlayers;
                mafiansAim = 0;
                pijawicaMrakoszlaps = 0;
                sklenarMirrors.Clear();
                wakedPlayers.Clear();
                grabarzMrakoszlaps = 0;
                this.Invoke((MethodInvoker)delegate
                {
                    yesButton.Enabled = false;
                    noButton.Enabled = false;
                    drawPlayers();
                    shotButton.Text = text[5, lang];
                    InfoRTB.Text = raport + text[6, lang] + endl;
                    InfoLabel.Focus();
                });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public int night()
        {
            try
            {
                tunel1 = -1;
                tunel2 = -1;
                zachroniony = -1;
                ofiara = -1;
                this.Invoke((MethodInvoker)delegate
                {
                    drawPlayers();
                    zapiszCoSeDzialoDoTxt();
                });
                //initializing + budzyni mafii na poczontku pjyrszej nocy
                if (nightPhase == cardNightPhases[(int)cardTypeNumber.jailer])
                {
                    //initializing
                    initNight();
                    //jailer
                    if (evenNight)
                    {
                        int cardType = (int)cardTypeNumber.jailer;
                        int cardNumber = 0;
                        int player = cards[cardType].cards[cardNumber].player;
                        player = cards[cardType].cards[cardNumber].player;
                        string name = nameOfCard(cardType, cardNumber);
                        if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                            wants(player, name);
                            if (endNight) { nightPhase = 25; return 0; }
                            if (undo) { return 2; }
                            if (wantsUse)
                            {
                                this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[7, lang]; });
                                clickPlayer(player, "", true, cardType);
                                if (undo) { return 2; }
                                players[clickedPlayer].wake = false;
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[clickedPlayer].name + ' ' + text[9, lang] + endl; InfoLabel.Focus(); });
                                cards[cardType].cards[cardNumber].uses--;
                            }
                        }
                    }
                    //budzyni mafii na poczontku pjyrszej nocy
                    else if (numOfNight == 1)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[76, lang] + endl; InfoLabel.Focus(); });
                        wants2(text[77, lang]);
                        if (endNight)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                undoButton.Enabled = true;
                                undoButton.Visible = true;
                            });
                            nightPhase = 25; return 0;
                        }
                        if (undo)
                        {
                            restoreGameState(gameStates.Count - 1);
                            return 0;
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            undoButton.Enabled = true;
                            undoButton.Visible = true;
                        });
                    }
                }
                //grabarz
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.grabarz])
                {
                    int cardType = (int)cardTypeNumber.grabarz;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                        wants(player, name);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            if (wantsUse)
                            {
                                grabarz = true;
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[12, lang] + endl; InfoLabel.Focus(); });
                                cards[cardType].cards[cardNumber].uses--;
                            }
                        }
                    }
                }
                //matrix
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.matrix])
                {
                    int cardType = (int)cardTypeNumber.matrix;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                        wants(player, name);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            if (wantsUse)
                            {
                                matrix = true;
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[13, lang] + endl; InfoLabel.Focus(); });
                                cards[cardType].cards[cardNumber].uses--;
                            }
                        }
                    }
                }
                //mag 1, 2 a 3
                else if (nightPhase >= cardNightPhases[(int)cardTypeNumber.mag] && nightPhase <= cardNightPhases[(int)cardTypeNumber.mag] + 2)
                {
                    int cardType = (int)cardTypeNumber.mag;
                    int cardNumber = nightPhase - cardNightPhases[(int)cardTypeNumber.mag];
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive)
                    {
                        if (!wakedPlayers.Contains(player))
                        {
                            wakedPlayers.Add(player);
                        }
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[14, lang]; });
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        tunel1 = clickedPlayer;
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[15, lang]; });
                        clickPlayer(player, name, false, cardType);
                        if (undo) { return 2; }
                        tunel2 = clickedPlayer;
                        if (players[player].wake)
                        {
                            addTunnel(tunel1, tunel2);
                        }
                    }
                }
                //slina 1 a 2
                else if (nightPhase >= cardNightPhases[(int)cardTypeNumber.slina] && nightPhase <= cardNightPhases[(int)cardTypeNumber.slina] + 1)
                {
                    int cardType = (int)cardTypeNumber.slina;
                    int cardNumber = nightPhase - cardNightPhases[(int)cardTypeNumber.slina];
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[16, lang]; });
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            players[clickedPlayer].hasSlina = true;
                            report[2] = text[17, lang] + players[clickedPlayer].name + ". ";
                            this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[17, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                        }
                    }
                }
                //pijavica
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.pijavica])
                {
                    int cardType = (int)cardTypeNumber.pijavica;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[18, lang]; });
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            players[clickedPlayer].hasPijavica = true;
                            this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[19, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                        }
                    }
                }
                //piosek
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.piosek])
                {
                    int cardType = (int)cardTypeNumber.piosek;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[20, lang]; });
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            if ((cards[(int)cardTypeNumber.mag].numInGame >= 2 && cards[(int)cardTypeNumber.mag].cards[1].player == clickedPlayer) || (cards[(int)cardTypeNumber.szilenyStrzelec].numInGame >= 2 && cards[(int)cardTypeNumber.szilenyStrzelec].cards[1].player == clickedPlayer) || (cards[(int)cardTypeNumber.alCapone].numInGame >= 1 && cards[(int)cardTypeNumber.alCapone].cards[0].player == clickedPlayer))
                            {
                                this.Invoke((MethodInvoker)delegate { report[3] = text[21, lang]; Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[clickedPlayer].name + text[22, lang] + endl; InfoLabel.Focus(); });
                            }
                            else
                            {
                                players[clickedPlayer].hasPiosek = true;
                                report[3] = text[23, lang] + players[clickedPlayer].name + ".";
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[23, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                            }
                        }
                    }
                }
                //Duch Bobo - w parzystej albo po tym co umrzil
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.duchBobo])
                {
                    int cardType = (int)cardTypeNumber.duchBobo;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && ((players[player].alive && evenNight) || (!players[player].alive && !duchBoboExhibowolPoUmrziciu)))
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[28, lang]; });
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (!wakedPlayers.Contains(player))
                        {
                            wakedPlayers.Add(player);
                        }
                        players[clickedPlayer].hasExhib = true;
                        report[1] = text[30, lang] + players[clickedPlayer].name + ". ";
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[29, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                        duchBoboExhibowolPoUmrziciu = true;
                    }
                }
                //doktor
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.doktor])
                {
                    int cardType = (int)cardTypeNumber.doktor;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        if ((numOfNight + 2 + posunyciDoktora) % 3 == 0)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[31, lang]; });
                        }
                        else
                        {
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[32, lang]; });
                        }
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            if (cards[(int)cardTypeNumber.mafian].numInGame + 1 == numberOfAlivePlayers && (numOfNight + 2 + posunyciDoktora) % 3 != 0)
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[clickedPlayer].name + text[33, lang] + endl; InfoLabel.Focus(); });
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[clickedPlayer].name + text[34, lang] + endl; InfoLabel.Focus(); });
                                players[clickedPlayer].hasDoktor = true;
                            }
                        }
                    }
                }
                //jozin z bazin
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.jozinZBazin])
                {
                    int cardType = (int)cardTypeNumber.jozinZBazin;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                        wants(player, name);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        while (cards[cardType].cards[cardNumber].uses > 0 && wantsUse && players[player].wake)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[35, lang]; });
                            clickPlayer(player, name, false, cardType);
                            if (undo) { return 2; }
                            this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[clickedPlayer].name + text[36, lang] + endl; InfoLabel.Focus(); });
                            cards[cardType].cards[cardNumber].uses--;
                            players[clickedPlayer].numberOfBloto++;

                            if (cards[cardType].cards[cardNumber].uses > 0)
                            {
                                this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[37, lang] + name + text[38, lang] + endl; InfoLabel.Focus(); });
                                wants(player, name);
                                if (undo) { return 2; }
                            }
                        }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                        }
                    }
                }
                //mafian 1, 2 a 3
                else if (nightPhase >= cardNightPhases[(int)cardTypeNumber.mafian] && nightPhase <= cardNightPhases[(int)cardTypeNumber.mafian] + 2)
                {
                    int cardType = (int)cardTypeNumber.mafian;
                    int cardNumber = nightPhase - cardNightPhases[(int)cardTypeNumber.mafian];
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[39, lang]; });
                        clickPlayer(player, name, true, cardType);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            if (mafiaShoots == numberOfPlayers)
                            {
                                mafiaShoots = clickedPlayer;
                            }
                            else if (mafiaShoots != numberOfPlayers && mafiaShoots != -1 && clickedPlayer != mafiaShoots)
                            {
                                mafiaShoots = -1;
                            }
                            mafiansAim++;
                            this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[40, lang] + ' ' + (cardNumber + 1) + ' ' + players[player].name + text[41, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                        }
                        else
                        {
                            mafiaShoots = -1;
                            this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[42, lang] + endl; InfoLabel.Focus(); });
                        }
                        if (mafiansAim == cards[(int)cardTypeNumber.mafian].numInGame)
                        {
                            if (mafiaShoots == -1)
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[43, lang] + endl; InfoLabel.Focus(); });
                            }
                            else
                            {
                                Shot shot = new Shot();
                                shot.type = 0;
                                shot.target = clickedPlayer; shot.from = player; shot.mafia = true; shot.reportNumber = 4; shot.first = true; shot.sniper = false;
                                shot.textInfo2RTB = ">>> " + text[40, lang] + ' ' + (cardNumber + 1) + ' ' + players[player].name + text[44, lang] + players[clickedPlayer].name + "." + endl;
                                shots.Add(shot);
                            }
                        }
                    }
                }
                //sniper
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.sniper])
                {
                    int cardType = (int)cardTypeNumber.sniper;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        resetBullet();
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                        wants(player, name);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (wantsUse)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[45, lang]; });
                            clickPlayer(player, name, false, cardType);
                            if (undo) { return 2; }
                            if (players[player].wake)
                            {
                                cards[cardType].cards[cardNumber].uses--;
                                Shot shot = new Shot();
                                shot.type = 0;
                                shot.target = clickedPlayer; shot.from = player; shot.mafia = false; shot.reportNumber = 7; shot.first = true; shot.sniper = true;
                                shot.textInfo2RTB = ">>> " + text[47, lang] + players[player].name + text[44, lang] + players[clickedPlayer].name + "." + endl;
                                shots.Add(shot);
                            }
                        }
                        if (players[player].wake && !wakedPlayers.Contains(player))
                        {
                            wakedPlayers.Add(player);
                        }
                    }
                }
                //fusekla
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.fusekla])
                {
                    int cardType = (int)cardTypeNumber.fusekla;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        resetBullet();
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                        wants(player, name);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                            if (wantsUse)
                            {
                                Shot shot = new Shot();
                                shot.type = 2;
                                shot.targetRight = getRightPlayer(player);
                                shot.targetLeft = getLeftPlayer(player);
                                shot.textInfo2RTB = ">>> " + text[48, lang] + endl;
                                shots.Add(shot);
                                cards[cardType].cards[cardNumber].uses--;
                            }
                        }
                    }
                }
                //slepy kat
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.slepyKat])
                {
                    int cardType = (int)cardTypeNumber.slepyKat;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + text[11, lang] + endl; InfoLabel.Focus(); });
                        wants(player, name);
                        if (endNight) { nightPhase = 25; return 0; }
                        if (undo) { return 2; }
                        if (wantsUse)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[52, lang]; });
                            clickPlayer(player, name, true, cardType);
                            if (undo) { return 2; }
                            zachroniony = clickedPlayer;
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[53, lang]; });
                            clickPlayer(player, name, false, cardType);
                            if (undo) { return 2; }
                            ofiara = clickedPlayer;
                            if (players[player].wake)
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[zachroniony].name + text[54, lang] + players[ofiara].name + "." + endl; InfoLabel.Focus(); });
                                cards[cardType].cards[cardNumber].uses--;
                                players[zachroniony].zachronionyKatym = true;
                                players[ofiara].ofiaraKata = true;
                            }
                        }
                        if (players[player].wake)
                        {
                            if (!wakedPlayers.Contains(player))
                            {
                                wakedPlayers.Add(player);
                            }
                        }
                    }
                }
                //luneta
                else if (nightPhase == cardNightPhases[(int)cardTypeNumber.luneta])
                {
                    int cardType = (int)cardTypeNumber.luneta;
                    int cardNumber = 0;
                    string name = nameOfCard(cardType, cardNumber);
                    int player = cards[cardType].cards[cardNumber].player;
                    if ((numOfNight + 1) % 3 == 0 && cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                    {
                        this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        if (players[player].wake)
                        {
                            this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[55, lang] + endl; InfoLabel.Focus(); });
                            int zmiana1, zmiana2, pomoc;
                            int iloscGraczowKierziSeObudzili = wakedPlayers.Count;
                            for (int i = 0; i < 50000; i++)
                            {
                                zmiana1 = rnd.Next(iloscGraczowKierziSeObudzili);
                                zmiana2 = rnd.Next(iloscGraczowKierziSeObudzili);
                                if (zmiana1 != zmiana2)
                                {
                                    pomoc = wakedPlayers[zmiana1];
                                    wakedPlayers[zmiana1] = wakedPlayers[zmiana2];
                                    wakedPlayers[zmiana2] = pomoc;
                                    pomoc = wakedPlayers[zmiana1];
                                    wakedPlayers[zmiana1] = wakedPlayers[zmiana2];
                                    wakedPlayers[zmiana2] = pomoc;
                                }
                            }
                            for (int i = 0; i < iloscGraczowKierziSeObudzili; i++)
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[56, lang] + players[wakedPlayers[i]].name + "." + endl; InfoLabel.Focus(); });
                            }
                            wants2(text[57, lang]);
                            if (endNight) { nightPhase = 25; return 0; }
                            if (undo) { return 2; }
                        }
                    }
                }
                //strzilani + koniec nocy
                else if (nightPhase == 25)
                {
                    // normal shots
                    List<Shot> shotsToRemove = new List<Shot>();
                    foreach (Shot shot in shots)
                    {
                        if (shot.type == 0)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                Info2RTB.Text += shot.textInfo2RTB;
                            });
                            resetBullet();
                            if (players[shot.target].alive)
                            {
                                shoot(shot.target, shot.from, shot.mafia, shot.reportNumber, shot.first, shot.sniper);
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[shot.target].name + text[58, lang] + endl; InfoLabel.Focus(); });
                                report[shot.reportNumber] = text[59, lang];
                            }
                            shotsToRemove.Add(shot);
                        }
                    }
                    foreach (Shot shot in shotsToRemove)
                    {
                        shots.Remove(shot);
                    }
                    shotsToRemove.Clear();
                    // matrix shots initializing
                    if (matrix)
                    {
                        int cardType = (int)cardTypeNumber.matrix;
                        int cardNumber = 0;
                        string name = nameOfCard(cardType, cardNumber);
                        int player = cards[cardType].cards[cardNumber].player;
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[60, lang] + matrixBullets + "." + endl; InfoLabel.Focus(); });
                        report[8] = text[61, lang] + matrixBullets + ".";
                        if (matrixBullets > 0)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                        }
                        for (int i = 0; i < matrixBullets; i++)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[45, lang]; });
                            clickPlayer(player, "", false, cardType);
                            if (undo) { return 2; }
                            report[9 + i] = text[62, lang] + (i + 1) + ", ";
                            Shot shot = new Shot();
                            shot.type = 1;
                            shot.target = clickedPlayer; shot.from = player; shot.bulletNumber = i;
                            shot.textInfo2RTB = ">>> " + text[63, lang] + players[player].name + text[44, lang] + players[clickedPlayer].name + "." + endl;
                            shots.Add(shot);
                            if (matrixBullets - i - 1 > 0)
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[64, lang] + (matrixBullets - i - 1) + "." + endl; InfoLabel.Focus(); });
                            }
                        }
                        matrix = false;
                    }
                    // matrix shots and fusekla
                    foreach (Shot shot in shots)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += shot.textInfo2RTB;
                        });
                        if (shot.type == 1)
                        {
                            resetBullet();
                            if (players[shot.target].alive)
                            {
                                matrixShoot(shot.target, shot.from, shot.bulletNumber);
                            }
                            else
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[shot.target].name + text[58, lang] + endl; InfoLabel.Focus(); });
                            }
                        }
                        else if (shot.type == 2)
                        {
                            toxic(shot.targetRight, shot.targetLeft);
                        }
                    }
                    shots.Clear();
                    // kuskona
                    int playerWithKuskona = cards[(int)cardTypeNumber.kusKona].cards[0].player;
                    if (playerWithKuskona != -1 && players[playerWithKuskona].alive && kuskonaZyskolMraka)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[65, lang] + ' ' + players[playerWithKuskona].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                        });
                        report[27] += text[65, lang] + text[66, lang] + (nextMrakoszlap + 1) + ". ";
                        players[playerWithKuskona].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                        players[playerWithKuskona].cardNumbers.Add(nextMrakoszlap);
                        cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithKuskona, 1, true));
                        cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                        nextMrakoszlap++;
                    }
                    // gandalf
                    int playerWithGandalf = cards[(int)cardTypeNumber.gandalf].cards[0].player;
                    if (playerWithGandalf != -1 && players[playerWithGandalf].alive && gandalfZyskolMraka)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[67, lang] + ' ' + players[playerWithGandalf].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                        });
                        report[26] += text[67, lang] + text[66, lang] + (nextMrakoszlap + 1) + ". ";
                        players[playerWithGandalf].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                        players[playerWithGandalf].cardNumbers.Add(nextMrakoszlap);
                        cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithGandalf, 1, true));
                        cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                        nextMrakoszlap++;
                    }
                    // pijawica
                    int playerWithPijavica = cards[(int)cardTypeNumber.pijavica].cards[0].player;
                    if (playerWithPijavica != -1 && players[playerWithPijavica].alive)
                    {
                        for (int i = 0; i < pijawicaMrakoszlaps; i++)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                Info2RTB.Text += text[69, lang] + ' ' + players[playerWithPijavica].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                            });
                            report[22] += text[69, lang] + text[68, lang] + (nextMrakoszlap + 1) + ". ";
                            players[playerWithPijavica].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                            players[playerWithPijavica].cardNumbers.Add(nextMrakoszlap);
                            cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithPijavica, 1, true));
                            cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                            nextMrakoszlap++;
                        }
                    }
                    // kobra
                    int playerWithKobra = cards[(int)cardTypeNumber.kobra].cards[0].player;
                    if (playerWithKobra != -1 && players[playerWithKobra].alive)
                    {
                        if (players[playerWithPijavica].hasKobra)
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                Info2RTB.Text += text[70, lang] + ' ' + players[playerWithKobra].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                            });
                            report[23] = text[70, lang] + text[68, lang] + (nextMrakoszlap + 1) + text[71, lang];
                            players[playerWithKobra].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                            players[playerWithKobra].cardNumbers.Add(nextMrakoszlap);
                            cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithKobra, 1, true));
                            cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                            nextMrakoszlap++;
                            cards[(int)cardTypeNumber.pijavica].cards[0].uses--;
                            cards[(int)cardTypeNumber.kobra].cards[0].uses--;
                        }
                    }
                    // grabarz
                    int playerWithGrabarz = cards[(int)cardTypeNumber.grabarz].cards[0].player;
                    if (playerWithGrabarz != -1 && players[playerWithGrabarz].alive)
                    {
                        if (grabarz)
                        {
                            for (int i = 0; i < grabarzMrakoszlaps; i++)
                            {
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Info2RTB.Text += text[72, lang] + ' ' + players[playerWithGrabarz].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                                });
                                report[24] += text[72, lang] + text[66, lang] + (nextMrakoszlap + 1) + ". ";
                                players[playerWithGrabarz].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                                players[playerWithGrabarz].cardNumbers.Add(nextMrakoszlap);
                                cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithGrabarz, 1, true));
                                cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                                nextMrakoszlap++;
                            }
                        }
                    }
                    // sklenar
                    int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                    if (playerWithSklenar != -1 && players[playerWithSklenar].alive)
                    {
                        if (sklenarMirrors.Count > 0 && !players[playerWithSklenar].cardTypes.Contains((int)cardTypeNumber.zwierciadlo))
                        {
                            this.Invoke((MethodInvoker)delegate
                            {
                                Info2RTB.Text += text[74, lang] + ' ' + players[playerWithSklenar].name + text[73, lang] + (nextZwierciadlo + 1) + "." + endl; InfoLabel.Focus();
                            });
                            report[25] = text[74, lang] + text[73, lang] + (nextZwierciadlo + 1) + ".";
                            players[playerWithSklenar].cardTypes.Add((int)cardTypeNumber.zwierciadlo);
                            players[playerWithSklenar].cardNumbers.Add(nextZwierciadlo);
                            cards[(int)cardTypeNumber.zwierciadlo].cards.Add(new Card(cardNames[(int)cardTypeNumber.zwierciadlo, lang], playerWithSklenar, 1, true));
                            cards[(int)cardTypeNumber.zwierciadlo].numInGame++;
                            nextZwierciadlo++;
                        }
                    }

                    drawPlayersCardsRTB();
                    this.Invoke((MethodInvoker)delegate
                    {
                        zapiszCoSeDzialoDoTxt();
                        buttonStartNight.Enabled = true;
                        buttonStartNight.Visible = true;
                        buttonStartNight.Text = text[5, lang];
                        yesButton.Enabled = false;
                        yesButton.Visible = false;
                        noButton.Enabled = false;
                        noButton.Visible = false;
                    });
                    if (endNight)
                    {
                        return 1;
                    }
                    wants2("");
                    report[0] = text[75, lang] + endl;
                    if (undo || wantsUse)
                    {
                        return 1;
                    }
                }
                else if (evenNight)
                {
                    //kobra
                    if (nightPhase == cardNightPhases[(int)cardTypeNumber.kobra])
                    {
                        int cardType = (int)cardTypeNumber.kobra;
                        int cardNumber = 0;
                        string name = nameOfCard(cardType, cardNumber);
                        int player = cards[cardType].cards[cardNumber].player;
                        if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[24, lang]; });
                            clickPlayer(player, name, true, cardType);
                            if (endNight) { nightPhase = 25; return 0; }
                            if (undo) { return 2; }
                            if (players[player].wake)
                            {
                                if (!wakedPlayers.Contains(player))
                                {
                                    wakedPlayers.Add(player);
                                }
                                players[clickedPlayer].hasKobra = true;
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[25, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                            }
                        }
                    }
                    //magnet
                    else if (nightPhase == cardNightPhases[(int)cardTypeNumber.magnet])
                    {
                        int cardType = (int)cardTypeNumber.magnet;
                        int cardNumber = 0;
                        string name = nameOfCard(cardType, cardNumber);
                        int player = cards[cardType].cards[cardNumber].player;
                        if (cards[cardType].cards[cardNumber].inGame && players[player].alive)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[26, lang]; });
                            clickPlayer(player, name, true, cardType);
                            if (endNight) { nightPhase = 25; return 0; }
                            if (undo) { return 2; }
                            if (players[player].wake)
                            {
                                if (!wakedPlayers.Contains(player))
                                {
                                    wakedPlayers.Add(player);
                                }
                                players[clickedPlayer].hasMagnet = true;
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[27, lang] + players[clickedPlayer].name + "." + endl; InfoLabel.Focus(); });
                            }
                        }
                    }
                    //szileny strzelec 1 a 2
                    else if (nightPhase >= cardNightPhases[(int)cardTypeNumber.szilenyStrzelec] && nightPhase <= cardNightPhases[(int)cardTypeNumber.szilenyStrzelec] + 1)
                    {
                        int cardType = (int)cardTypeNumber.szilenyStrzelec;
                        int cardNumber = nightPhase - cardNightPhases[(int)cardTypeNumber.szilenyStrzelec];
                        string name = nameOfCard(cardType, cardNumber);
                        int player = cards[cardType].cards[cardNumber].player;
                        if (cards[cardType].cards[cardNumber].inGame && players[player].alive)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[45, lang]; });
                            clickPlayer(player, name, true, cardType);
                            if (endNight) { nightPhase = 25; return 0; }
                            if (undo) { return 2; }
                            if (players[player].wake)
                            {
                                if (!wakedPlayers.Contains(player))
                                {
                                    wakedPlayers.Add(player);
                                }
                                Shot shot = new Shot();
                                shot.type = 0;
                                shot.target = clickedPlayer; shot.from = player; shot.mafia = false; shot.reportNumber = 5 + cardNumber; shot.first = true; shot.sniper = false;
                                shot.textInfo2RTB = ">>> " + text[46, lang] + ' ' + (cardNumber + 1) + ' ' + players[player].name + text[44, lang] + players[clickedPlayer].name + "." + endl;
                                shots.Add(shot);
                            }
                        }
                    }
                    //soudce
                    else if (nightPhase == cardNightPhases[(int)cardTypeNumber.soudce])
                    {
                        int cardType = (int)cardTypeNumber.soudce;
                        int cardNumber = 0;
                        string name = nameOfCard(cardType, cardNumber);
                        int player = cards[cardType].cards[cardNumber].player;
                        if (cards[cardType].cards[cardNumber].inGame && players[player].alive && cards[cardType].cards[cardNumber].uses > 0)
                        {
                            this.Invoke((MethodInvoker)delegate { InfoRTB.Text += text[10, lang] + name + '.' + endl; InfoLabel.Focus(); });
                            this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[49, lang]; });
                            clickPlayer(player, name, false, cardType);
                            if (endNight) { nightPhase = 25; return 0; }
                            if (undo) { return 2; }
                            if (players[player].wake)
                            {
                                if (!wakedPlayers.Contains(player))
                                {
                                    wakedPlayers.Add(player);
                                }
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[8, lang] + ' ' + players[clickedPlayer].name + text[50, lang] + endl; InfoLabel.Focus(); });
                                report[28] = text[51, lang] + players[clickedPlayer].name + ". ";
                                players[clickedPlayer].hasZakazGlosowanio = true;
                            }
                        }
                    }
                }
                if (!undo) { nightPhase++; } else { restoreGameState(gameStates.Count - 2); }
                return 0;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
                nightPhase++;
            }
            return 0;
        }

        public int day()
        {
            try
            {
                isNight = false;
                endNight = false;
                kuskonaZyskolMraka = false;
                gandalfZyskolMraka = false;
                report[0] = text[75, lang] + endl;
                for (int i = 0; i < 3; i++)
                {
                    tunnels[i].from = -1;
                }
                resetBullet();
                numberOfTunnels = 0;
                this.Invoke((MethodInvoker)delegate { InfoRTB.Text = ""; Info2RTB.Text += endl + "-----------------------------" + endl + endl; InfoLabel.Focus(); });
                nightPhase = 0;
                evenNight = !evenNight;
                this.Invoke((MethodInvoker)delegate { drawPlayers(); });
                string raport = "";
                for (int i = 0; i < numberOfReports; i++)
                {
                    if (report[i] != "")
                    {
                        raport += report[i] + endl;
                        report[i] = "";
                    }
                }
                isNight = false;
                this.Invoke((MethodInvoker)delegate
                {
                    yesButton.Enabled = false;
                    yesButton.Visible = false;
                    noButton.Enabled = false;
                    noButton.Visible = false;
                    speedLabel.Visible = false;
                    speedTrackBar.Enabled = false;
                    speedTrackBar.Visible = false;
                    flyingCheckBox.Enabled = false;
                    flyingCheckBox.Visible = false;
                    InfoRTB.Text = raport;
                    InfoLabel.Focus();
                    votedButton.Enabled = true;
                    votedButton.Visible = true;
                    shotButton.Enabled = true;
                    shotButton.Text = text[78, lang];
                    shotButton.Visible = true;
                    buttonStartNight.Enabled = true;
                    buttonStartNight.Text = text[79, lang];
                    buttonStartNight.Visible = true;
                    addCardButton.Enabled = true;
                    addCardButton.Visible = true;
                    removeCardButton.Enabled = true;
                    removeCardButton.Visible = true;
                    bombButton.Enabled = true;
                    bombButton.Visible = true;
                });

                wants2("");
                if (undo) { return 0; }
                if (wantsUse)
                {
                    canClickPictureBox = false;
                    // kuskona
                    int playerWithKuskona = cards[(int)cardTypeNumber.kusKona].cards[0].player;
                    if (playerWithKuskona != -1 && players[playerWithKuskona].alive && kuskonaZyskolMraka)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[65, lang] + ' ' + players[playerWithKuskona].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                        });
                        report[27] += text[65, lang] + text[66, lang] + (nextMrakoszlap + 1) + ". ";
                        players[playerWithKuskona].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                        players[playerWithKuskona].cardNumbers.Add(nextMrakoszlap);
                        cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithKuskona, 1, true));
                        cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                        nextMrakoszlap++;
                    }
                    // gandalf
                    int playerWithGandalf = cards[(int)cardTypeNumber.gandalf].cards[0].player;
                    if (playerWithGandalf != -1 && players[playerWithGandalf].alive && gandalfZyskolMraka)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[67, lang] + ' ' + players[playerWithGandalf].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                        });
                        report[26] += text[67, lang] + text[66, lang] + (nextMrakoszlap + 1) + ". ";
                        players[playerWithGandalf].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                        players[playerWithGandalf].cardNumbers.Add(nextMrakoszlap);
                        cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], playerWithGandalf, 1, true));
                        cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                        nextMrakoszlap++;
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        votedButton.Enabled = false;
                        votedButton.Visible = false;
                        buttonStartNight.Enabled = false;
                        buttonStartNight.Visible = false;
                        addCardButton.Enabled = false;
                        addCardButton.Visible = false;
                        removeCardButton.Enabled = false;
                        removeCardButton.Visible = false;
                        bombButton.Enabled = false;
                        bombButton.Visible = false;
                        yesButton.Enabled = true;
                        yesButton.Visible = true;
                        noButton.Enabled = true;
                        noButton.Visible = true;
                        speedLabel.Visible = true;
                        speedTrackBar.Enabled = true;
                        speedTrackBar.Visible = true;
                        flyingCheckBox.Enabled = true;
                        flyingCheckBox.Visible = true;
                        
                    });
                    return 0;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
                day();
            }
            return 0;
        }

        // functions for undoing stuff

        public void saveGameState()
        {
            try
            {
                gameState state = new gameState();
                state.players = new Player[numberOfPlayers];
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    state.players[i] = DeepClone(players[i]);
                }
                state.cards = new Cards[numOfSpecificCards];
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    state.cards[i] = DeepClone(cards[i]);
                }
                state.addRemoveCard = addRemoveCard;
                state.bullet = bullet;
                state.canClickPictureBox = canClickPictureBox;
                state.clickedPlayer = clickedPlayer;
                state.duchBoboExhibowolPoUmrziciu = duchBoboExhibowolPoUmrziciu;
                state.evenNight = evenNight;
                state.gandalfZyskolMraka = gandalfZyskolMraka;
                state.grabarz = grabarz;
                state.grabarzMrakoszlaps = grabarzMrakoszlaps;
                state.graczKíeryKliko = graczKíeryKliko;
                state.isNight = isNight;
                state.kuskonaZyskolMraka = kuskonaZyskolMraka;
                state.mafiansAim = mafiansAim;
                state.mafiaShoots = mafiaShoots;
                state.matrix = matrix;
                state.matrixBullets = matrixBullets;
                state.nextImunita = nextImunita;
                state.nextKewlar = nextKewlar;
                state.nextMrakoszlap = nextMrakoszlap;
                state.nextProwazochodec = nextProwazochodec;
                state.nextZwierciadlo = nextZwierciadlo;
                state.nightPhase = nightPhase;
                state.numberOfAlivePlayers = numberOfAlivePlayers;
                state.numberOfTunnels = numberOfTunnels;
                state.numOfAllCards = numOfAllCards;
                state.numOfNight = numOfNight;
                state.ofiara = ofiara;
                state.pijawicaMrakoszlaps = pijawicaMrakoszlaps;
                state.players = players;
                state.playersNamesSet = playersNamesSet;
                state.posunyciDoktora = posunyciDoktora;
                state.report = report;
                state.selectedPlayer = selectedPlayer;
                state.shots = DeepClone(shots);
                state.sklenarMirrors = DeepClone(sklenarMirrors);
                state.starterThread = starterThread;
                state.tunel1 = tunel1;
                state.tunel2 = tunel2;
                state.tunnels = tunnels;
                state.votedShot = votedShot;
                state.wakedPlayers = DeepClone(wakedPlayers);
                state.wantsUse = wantsUse;
                state.zachroniony = zachroniony;
                //state.firstNightPhase = firstNightPhase;
                this.Invoke((MethodInvoker)delegate
                {
                    state.InfoRTBText = InfoRTB.Text;
                    state.Info2RTBText = Info2RTB.Text;
                    state.yesButtonEnabled = yesButton.Enabled;
                    state.yesButtonVisible = yesButton.Visible;
                    state.noButtonEnabled = noButton.Enabled;
                    state.noButtonVisible = noButton.Visible;
                    state.votedButtonEnabled = votedButton.Enabled;
                    state.votedButtonVisible = votedButton.Visible;
                    state.shotButtonEnabled = shotButton.Enabled;
                    state.shotButtonVisible = shotButton.Visible;
                    state.shotButtonText = shotButton.Text;
                    state.buttonStartNightEnabled = buttonStartNight.Enabled;
                    state.buttonStartNightVisible = buttonStartNight.Visible;
                    state.buttonStartNightText = buttonStartNight.Text;
                    state.addCardButtonEnabled = addCardButton.Enabled;
                    state.addCardButtonVisible = addCardButton.Visible;
                    state.removeCardButtonEnabled = removeCardButton.Enabled;
                    state.removeCardButtonVisible = removeCardButton.Visible;
                    state.addCardComboboxEnabled = addCardCombobox.Enabled;
                    state.addCardComboboxVisible = addCardCombobox.Visible;
                    state.removeCardComboboxEnabled = removeCardCombobox.Enabled;
                    state.removeCardComboboxVisible = removeCardCombobox.Visible;
                    state.undoButtonEnabled = undoButton.Enabled;
                    state.undoButtonVisible = undoButton.Visible;
                    state.bombButtonEnabled = bombButton.Enabled;
                    state.bombButtonVisible = bombButton.Visible;
                    state.PlayersCardsRichTextBoxText = PlayersCardsRichTextBox.Text;
                    state.speedLabelVisible = speedLabel.Visible;
                    state.speedTrackBarEnabled = speedTrackBar.Enabled;
                    state.speedTrackBarVisible = speedTrackBar.Visible;
                    state.flyingCheckBoxEnabled = flyingCheckBox.Enabled;
                    state.flyingCheckBoxVisible = flyingCheckBox.Visible;
                });

                gameStates.Add(DeepClone(state));
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void restoreGameState(int numberOfGameState)
        {
            try
            {
                gameState state = gameStates[numberOfGameState];
                for (int i = 0; i < numOfSpecificCards; i++)
                {
                    cards[i] = DeepClone(state.cards[i]);
                }
                addRemoveCard = state.addRemoveCard;
                bullet = state.bullet;
                canClickPictureBox = state.canClickPictureBox;
                clickedPlayer = state.clickedPlayer;
                duchBoboExhibowolPoUmrziciu = state.duchBoboExhibowolPoUmrziciu;
                evenNight = state.evenNight;
                gandalfZyskolMraka = state.gandalfZyskolMraka;
                grabarz = state.grabarz;
                grabarzMrakoszlaps = state.grabarzMrakoszlaps;
                graczKíeryKliko = state.graczKíeryKliko;
                isNight = state.isNight;
                kuskonaZyskolMraka = state.kuskonaZyskolMraka;
                mafiansAim = state.mafiansAim;
                mafiaShoots = state.mafiaShoots;
                matrix = state.matrix;
                matrixBullets = state.matrixBullets;
                nextImunita = state.nextImunita;
                nextKewlar = state.nextKewlar;
                nextMrakoszlap = state.nextMrakoszlap;
                nextProwazochodec = state.nextProwazochodec;
                nextZwierciadlo = state.nextZwierciadlo;
                nightPhase = state.nightPhase;
                numberOfAlivePlayers = state.numberOfAlivePlayers;
                numberOfTunnels = state.numberOfTunnels;
                numOfAllCards = state.numOfAllCards;
                numOfNight = state.numOfNight;
                ofiara = state.ofiara;
                pijawicaMrakoszlaps = state.pijawicaMrakoszlaps;
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    players[i] = DeepClone(state.players[i]);
                }
                playersNamesSet = state.playersNamesSet;
                posunyciDoktora = state.posunyciDoktora;
                report = state.report;
                selectedPlayer = state.selectedPlayer;
                shots.Clear();
                foreach (Shot shot in state.shots)
                {
                    shots.Add(shot);
                }
                sklenarMirrors.Clear();
                foreach (int mirror in state.sklenarMirrors)
                {
                    sklenarMirrors.Add(mirror);
                }
                starterThread = state.starterThread;
                tunel1 = state.tunel1;
                tunel2 = state.tunel2;
                tunnels = state.tunnels;
                votedShot = state.votedShot;
                wakedPlayers.Clear();
                foreach (int wakedPlayer in state.wakedPlayers)
                {
                    wakedPlayers.Add(wakedPlayer);
                }
                wantsUse = state.wantsUse;
                zachroniony = state.zachroniony;
                this.Invoke((MethodInvoker)delegate
                {
                    InfoRTB.Text = state.InfoRTBText;
                    Info2RTB.Text = state.Info2RTBText;
                    yesButton.Enabled = state.yesButtonEnabled;
                    yesButton.Visible = state.yesButtonVisible;
                    noButton.Enabled = state.noButtonEnabled;
                    noButton.Visible = state.noButtonVisible;
                    votedButton.Enabled = state.votedButtonEnabled;
                    votedButton.Visible = state.votedButtonVisible;
                    shotButton.Enabled = state.shotButtonEnabled;
                    shotButton.Visible = state.shotButtonVisible;
                    shotButton.Text = state.shotButtonText;
                    buttonStartNight.Enabled = state.buttonStartNightEnabled;
                    buttonStartNight.Visible = state.buttonStartNightVisible;
                    buttonStartNight.Text = state.buttonStartNightText;
                    addCardButton.Enabled = state.addCardButtonEnabled;
                    addCardButton.Visible = state.addCardButtonVisible;
                    removeCardButton.Enabled = state.removeCardButtonEnabled;
                    removeCardButton.Visible = state.removeCardButtonVisible;
                    addCardCombobox.Enabled = state.addCardComboboxEnabled;
                    addCardCombobox.Visible = state.addCardComboboxVisible;
                    removeCardCombobox.Enabled = state.removeCardComboboxEnabled;
                    removeCardCombobox.Visible = state.removeCardComboboxVisible;
                    undoButton.Enabled = state.undoButtonEnabled;
                    undoButton.Visible = state.undoButtonVisible;
                    bombButton.Enabled = state.bombButtonEnabled;
                    bombButton.Visible = state.bombButtonVisible;
                    PlayersCardsRichTextBox.Text = state.PlayersCardsRichTextBoxText;
                    speedLabel.Visible = state.speedLabelVisible;
                    speedTrackBar.Enabled = state.speedTrackBarEnabled;
                    speedTrackBar.Visible = state.speedTrackBarVisible;
                    flyingCheckBox.Enabled = state.flyingCheckBoxEnabled;
                    flyingCheckBox.Visible = state.flyingCheckBoxVisible;
                });
                if (gameStates.Count >= 2)
                {
                    gameStates.RemoveAt(gameStates.Count - 1);
                    gameStates.RemoveAt(gameStates.Count - 1);
                }
                else if (gameStates.Count == 1)
                {
                    gameStates.RemoveAt(gameStates.Count - 1);
                }
                if (waitForClickPBset)
                {
                    waitForClickPB.Set();
                }
                if (waitForClickYesNoset)
                {
                    waitForClickYesNo.Set();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public static T DeepClone<T>(T obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (T)formatter.Deserialize(ms);
            }
        }

        // functions for shooting stuff

        //target - na niego leci pocisk, from - od niego leci pocisk, mafia - jesli mafia wystrzelila, 
        //first - jesli pocisk leci od gracza kiery niom wystrzelil, sniper - jesli sniper wystrzelil
        public void shoot(int target, int from, bool mafia, int reportNumber, bool first, bool sniper)
        {
            try
            {
                //updating report and drawing bullets
                if (first)
                {
                    report[reportNumber] = text[80, lang];
                }
                else if (players[target].alive)
                {
                    if (flyingCheckBox.Checked)
                    {
                        drawBulletShoot(players[from].position.x, players[from].position.y, players[target].position.x, players[target].position.y);
                    }
                    else
                    {
                        drawBullet(players[from].position.x, players[from].position.y, players[target].position.x, players[target].position.y);
                    }
                }
                //initializing left and right player, checking if tunnels arent made to dead players (same with magnets)
                int leftPlayer = getLeftPlayer(target);
                int rightPlayer = getRightPlayer(target);
                bool useTunnel = false;
                int rozszczep = -1;
                if (players[target].tunnelsFrom > 0)
                {
                    int t = 0;
                    for (int i = 0; i < players[target].tunnelsFrom; i++)
                    {
                        if (!bullet.usedTunnel[players[target].tunnels[i].numOfTunnel])
                        {
                            if (players[players[target].tunnels[i].to].alive)
                            {
                                t++;
                            }
                        }
                    }
                    if (t > 0)
                    {
                        useTunnel = true;
                        rozszczep = t;
                    }
                }
                //matrix
                if (matrix && players[target].cardTypes.Contains((int)cardTypeNumber.matrix))
                {
                    int item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.matrix);
                    matrixBullets++;
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[81, lang] + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[82, lang];
                }
                //magnet
                else if (leftPlayer != -1 && players[leftPlayer].hasMagnet && !bullet.usedMagnet && players[leftPlayer].alive)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[83, lang] + players[target].name + text[84, lang] + players[leftPlayer].name + "." + endl; InfoLabel.Focus();
                    });
                    bullet.usedMagnet = true;
                    report[reportNumber] += text[85, lang];
                    if (players[leftPlayer].alive)
                    {
                        shoot(leftPlayer, target, mafia, reportNumber, false, sniper);
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[leftPlayer].name + text[58, lang] + endl; InfoLabel.Focus(); });
                        report[reportNumber] = text[86, lang];
                    }
                }
                else if (rightPlayer != -1 && players[rightPlayer].hasMagnet && !bullet.usedMagnet && players[rightPlayer].alive)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[83, lang] + players[target].name + text[84, lang] + players[rightPlayer].name + "." + endl; InfoLabel.Focus();
                    });
                    bullet.usedMagnet = true;
                    report[reportNumber] += text[85, lang];
                    if (players[rightPlayer].alive)
                    {
                        shoot(rightPlayer, target, mafia, reportNumber, false, sniper);
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[rightPlayer].name + text[58, lang] + endl; InfoLabel.Focus(); });
                        report[reportNumber] += text[86, lang];
                    }
                }
                //tunel
                else if (useTunnel)
                {
                    if (rozszczep > 1)
                    {
                        for (int i = 0; i < players[target].tunnelsFrom; i++)
                        {
                            bullet.usedTunnel[players[target].tunnels[i].numOfTunnel] = true;
                        }
                    }
                    for (int i = 0; i < players[target].tunnelsFrom; i++)
                    {
                        if (!bullet.usedTunnel[players[target].tunnels[i].numOfTunnel] || rozszczep > 1)
                        {
                            if (rozszczep > 1)
                            {
                                if (i == 0)
                                {
                                    report[reportNumber] += text[87, lang] + rozszczep + text[88, lang];
                                }
                                report[reportNumber] += ", " + (i + 1) + text[151, lang];
                            }
                            if (players[players[target].tunnels[i].to].alive)
                            {
                                if (rozszczep == 1)
                                {
                                    report[reportNumber] += text[92, lang];
                                }
                                this.Invoke((MethodInvoker)delegate
                                {
                                    Info2RTB.Text += text[89, lang] + (players[target].tunnels[i].numOfTunnel + 1) + text[90, lang] + players[target].name + text[91, lang] + players[players[target].tunnels[i].to].name + "." + endl; InfoLabel.Focus();
                                });
                                bullet.usedTunnel[players[target].tunnels[i].numOfTunnel] = true;
                                shoot(players[target].tunnels[i].to, target, mafia, reportNumber, false, sniper);
                            }
                            else if (i == 0 || rozszczep > 1)
                            {
                                this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[players[target].tunnels[i].to].name + text[58, lang] + endl; InfoLabel.Focus(); });
                                report[reportNumber] += text[86, lang];
                            }
                        }
                    }
                }
                //slina
                else if (players[target].hasSlina && !players[target].hasPiosek)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[93, lang] + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[94, lang];
                }
                //al capone
                else if (players[target].cardTypes.Contains((int)cardTypeNumber.alCapone) && mafia)
                {
                    if (players[target].hasSlina && players[target].hasPiosek)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[95, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[96, lang] + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[97, lang];
                }
                //doktor
                else if (players[target].hasDoktor)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[98, lang] + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[99, lang];
                    players[target].hasDoktor = false;
                }
                //zwierciadlo
                else if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && !sniper && players[target].numberOfBloto == 0)
                {
                    if (players[target].hasSlina && players[target].hasPiosek)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[95, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    int item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                    int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                    if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                    {
                        sklenarMirrors.Add(players[target].cardNumbers[item]);
                    }
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                        + text[102, lang] + players[from].name + "." + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[104, lang];
                    players[target].cardTypes.RemoveAt(item);
                    players[target].cardNumbers.RemoveAt(item);
                    if (players[from].alive)
                    {
                        shoot(from, target, mafia, reportNumber, false, sniper);
                    }
                    else
                    {
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[from].name + text[58, lang] + endl; InfoLabel.Focus(); });
                        report[reportNumber] += text[86, lang];
                    }
                }
                //neprustrzelno westa
                else if (players[target].cardTypes.Contains(3))
                {
                    int item;
                    if (players[target].hasSlina && players[target].hasPiosek)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[95, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    //pocisk od snipera rozbila zwierciadlo
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && sniper)
                    {
                        item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                        int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                        if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                        {
                            sklenarMirrors.Add(players[target].cardNumbers[item]);
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                                + text[105, lang] + endl; InfoLabel.Focus();
                        });
                        report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[106, lang];
                        players[target].cardTypes.RemoveAt(item);
                        players[target].cardNumbers.RemoveAt(item);
                        if (players[target].numberOfBloto > 0)
                        {
                            players[target].numberOfBloto--;
                        }
                    }
                    //pocisk rozbila zwierciadlo pochlapane blotym
                    else if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && players[target].numberOfBloto > 0)
                    {
                        item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                        int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                        if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                        {
                            sklenarMirrors.Add(players[target].cardNumbers[item]);
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                                + text[107, lang] + endl; InfoLabel.Focus();
                        });
                        report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[106, lang];
                        players[target].cardTypes.RemoveAt(item);
                        players[target].cardNumbers.RemoveAt(item);
                        players[target].numberOfBloto--;
                    }
                    item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.neprustrzelnoWesta);
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[108, lang] + (players[target].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[109, lang] + (players[target].cardNumbers[item] + 1) + ".";
                    players[target].cardTypes.RemoveAt(item);
                    players[target].cardNumbers.RemoveAt(item);
                }
                //mrakoszlap
                else if (players[target].cardTypes.Contains(0))
                {
                    int item;
                    if (players[target].hasSlina && players[target].hasPiosek)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[95, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    //pocisk od snipera rozbil zwierciadlo
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && sniper)
                    {
                        item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                        int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                        if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                        {
                            sklenarMirrors.Add(players[target].cardNumbers[item]);
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                                + text[105, lang] + endl; InfoLabel.Focus();
                        });
                        report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[106, lang];
                        players[target].cardTypes.RemoveAt(item);
                        players[target].cardNumbers.RemoveAt(item);
                        if (players[target].numberOfBloto > 0)
                        {
                            players[target].numberOfBloto--;
                        }
                    }
                    //pocisk rozbil zwierciadlo pochlapane blotym
                    else if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && players[target].numberOfBloto > 0)
                    {
                        item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                        int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                        if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                        {
                            sklenarMirrors.Add(players[target].cardNumbers[item]);
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                                + text[107, lang] + endl; InfoLabel.Focus();
                        });
                        report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[106, lang];
                        players[target].cardTypes.RemoveAt(item);
                        players[target].cardNumbers.RemoveAt(item);
                        players[target].numberOfBloto--;
                    }
                    item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.mrakoszlap);
                    int mrakoszlap = players[target].cardNumbers[item];
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[116, lang] + (players[target].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                    });
                    report[reportNumber] += text[110, lang] + (players[target].cardNumbers[item] + 1) + ".";
                    if (players[target].hasPijavica)
                    {
                        pijawicaMrakoszlaps++;
                    }
                    //jesli to je gandalf, tak kuskona zyskuje mrakoszlap
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.gandalf) && !kuskonaZyskolMraka && players[cards[(int)cardTypeNumber.kusKona].cards[0].player].alive)
                    {
                        kuskonaZyskolMraka = true;
                    }
                    //jesli to je kuskona, tak gandalf zyskuje mrakoszlap
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.kusKona) && !gandalfZyskolMraka && players[cards[(int)cardTypeNumber.gandalf].cards[0].player].alive)
                    {
                        gandalfZyskolMraka = true;
                    }
                    //jesli grabarz pouzyl swojom funkcje, tak dostanie mrakoszlapa
                    if (grabarz)
                    {
                        grabarzMrakoszlaps++;
                    }
                    players[target].cardTypes.RemoveAt(item);
                    players[target].cardNumbers.RemoveAt(item);
                }
                //umrzil
                else
                {
                    if (players[target].hasSlina && players[target].hasPiosek)
                    {
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[95, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    //pocisk od snipera rozbil zwierciadlo
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && sniper)
                    {
                        int item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                        int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                        if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                        {
                            sklenarMirrors.Add(players[target].cardNumbers[item]);
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                                + text[105, lang] + endl; InfoLabel.Focus();
                        });
                        report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[106, lang];
                        players[target].cardTypes.RemoveAt(item);
                        players[target].cardNumbers.RemoveAt(item);
                        if (players[target].numberOfBloto > 0)
                        {
                            players[target].numberOfBloto--;
                        }
                    }
                    //pocisk rozbila zwierciadlo pochlapane blotym
                    else if (players[target].cardTypes.Contains((int)cardTypeNumber.zwierciadlo) && players[target].numberOfBloto > 0)
                    {
                        int item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.zwierciadlo);
                        int playerWithSklenar = cards[(int)cardTypeNumber.sklenar].cards[0].player;
                        if (sklenarMirrors.Count == 0 && target != playerWithSklenar)
                        {
                            sklenarMirrors.Add(players[target].cardNumbers[item]);
                        }
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[100, lang] + players[target].name + text[101, lang] + (players[target].cardNumbers[item] + 1)
                                + text[107, lang] + endl; InfoLabel.Focus();
                        });
                        report[reportNumber] += text[103, lang] + (players[target].cardNumbers[item] + 1) + text[106, lang];
                        players[target].cardTypes.RemoveAt(item);
                        players[target].cardNumbers.RemoveAt(item);
                        players[target].numberOfBloto--;
                    }
                    if (players[target].hasPijavica)
                    {
                        pijawicaMrakoszlaps++;
                    }
                    this.Invoke((MethodInvoker)delegate { drawPlayers(); });
                    death(target, reportNumber);
                }
                if (first)
                {
                    drawPlayersCardsRTB();
                    this.Invoke((MethodInvoker)delegate
                    {
                        drawPlayers();
                    });
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void toxic(int rightPlayer, int leftPlayer)
        {
            try
            {
                report[21] = text[111, lang];

                //gracz po lewej
                if (leftPlayer != -1 && players[leftPlayer].alive && ((cards[(int)cardTypeNumber.mag].numInGame >= 2 && cards[(int)cardTypeNumber.mag].cards[1].player == leftPlayer) || (cards[(int)cardTypeNumber.szilenyStrzelec].numInGame >= 2 && cards[(int)cardTypeNumber.szilenyStrzelec].cards[1].player == leftPlayer) || (cards[(int)cardTypeNumber.alCapone].numInGame >= 1 && cards[(int)cardTypeNumber.alCapone].cards[0].player == leftPlayer)))
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[leftPlayer].name + text[113, lang] + endl; InfoLabel.Focus();
                    });
                    report[21] += text[112, lang];
                }
                else if (leftPlayer != -1)
                {
                    //mortwy
                    if (!players[leftPlayer].alive)
                    {
                        report[21] += text[114, lang];
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[leftPlayer].name + text[58, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    //doktor
                    else if (players[leftPlayer].hasDoktor)
                    {
                        report[21] += text[115, lang];
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[leftPlayer].name + text[98, lang] + endl; InfoLabel.Focus();
                        });
                        players[leftPlayer].hasDoktor = false;
                    }
                    //mrakoszlap
                    else if (players[leftPlayer].cardTypes.Contains((int)cardTypeNumber.mrakoszlap))
                    {
                        int item = players[leftPlayer].cardTypes.FindIndex(x => x == (int)cardTypeNumber.mrakoszlap);
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[leftPlayer].name + text[116, lang] + (players[leftPlayer].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        });
                        report[21] += text[117, lang] + (players[leftPlayer].cardNumbers[item] + 1) + " ";
                        if (players[leftPlayer].hasPijavica)
                        {
                            pijawicaMrakoszlaps++;
                        }
                        //jesli to je gandalf, tak kuskona zyskuje mrakoszlap
                        if (players[leftPlayer].cardTypes.Contains((int)cardTypeNumber.gandalf) && !kuskonaZyskolMraka && players[cards[(int)cardTypeNumber.kusKona].cards[0].player].alive)
                        {
                            kuskonaZyskolMraka = true;
                        }
                        //jesli to je kuskona, tak gandalf zyskuje mrakoszlap
                        if (players[leftPlayer].cardTypes.Contains((int)cardTypeNumber.kusKona) && !gandalfZyskolMraka && players[cards[(int)cardTypeNumber.gandalf].cards[0].player].alive)
                        {
                            gandalfZyskolMraka = true;
                        }
                        //jesli grabarz pouzyl swojom funkcje, tak dostanie mrakoszlapa
                        if (grabarz)
                        {
                            grabarzMrakoszlaps++;
                        }
                        players[leftPlayer].cardTypes.RemoveAt(item);
                        players[leftPlayer].cardNumbers.RemoveAt(item);
                    }
                    //umrzil
                    else
                    {
                        if (players[leftPlayer].hasPijavica)
                        {
                            pijawicaMrakoszlaps++;
                        }
                        report[21] += text[118, lang] + players[leftPlayer].name + " ";
                        death(leftPlayer, -1);
                    }
                }
                //gracz po prawej
                if (numberOfAlivePlayers > 2 && rightPlayer != -1 && players[rightPlayer].alive && (((cards[(int)cardTypeNumber.mag].numInGame >= 2 && cards[(int)cardTypeNumber.mag].cards[1].player == rightPlayer) || (cards[(int)cardTypeNumber.szilenyStrzelec].numInGame >= 2 && cards[(int)cardTypeNumber.szilenyStrzelec].cards[1].player == rightPlayer) || (cards[(int)cardTypeNumber.alCapone].numInGame >= 1 && cards[(int)cardTypeNumber.alCapone].cards[0].player == rightPlayer))))
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[rightPlayer].name + text[113, lang] + endl; InfoLabel.Focus();
                    });
                    report[21] += text[119, lang];
                }
                else if (rightPlayer != -1 && numberOfAlivePlayers > 2)
                {
                    //mortwy
                    if (!players[rightPlayer].alive)
                    {
                        report[21] += text[120, lang];
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[rightPlayer].name + text[58, lang] + endl; InfoLabel.Focus();
                        });
                    }
                    //doktor
                    else if (players[rightPlayer].hasDoktor)
                    {
                        report[21] += text[121, lang];
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[rightPlayer].name + text[98, lang] + endl; InfoLabel.Focus();
                        });
                        players[rightPlayer].hasDoktor = false;
                    }
                    //mrakoszlap
                    else if (players[rightPlayer].cardTypes.Contains((int)cardTypeNumber.mrakoszlap))
                    {
                        int item = players[rightPlayer].cardTypes.FindIndex(x => x == (int)cardTypeNumber.mrakoszlap);
                        this.Invoke((MethodInvoker)delegate
                        {
                            Info2RTB.Text += text[8, lang] + ' ' + players[rightPlayer].name + text[116, lang] + (players[rightPlayer].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        });
                        report[21] += text[122, lang] + (players[rightPlayer].cardNumbers[item] + 1) + ".";
                        if (players[rightPlayer].hasPijavica)
                        {
                            pijawicaMrakoszlaps++;
                        }
                        //jesli to je gandalf, tak kuskona zyskuje mrakoszlap
                        if (players[rightPlayer].cardTypes.Contains((int)cardTypeNumber.gandalf) && !kuskonaZyskolMraka && players[cards[(int)cardTypeNumber.kusKona].cards[0].player].alive)
                        {
                            kuskonaZyskolMraka = true;
                        }
                        //jesli to je kuskona, tak gandalf zyskuje mrakoszlap
                        if (players[rightPlayer].cardTypes.Contains((int)cardTypeNumber.kusKona) && !gandalfZyskolMraka && players[cards[(int)cardTypeNumber.gandalf].cards[0].player].alive)
                        {
                            gandalfZyskolMraka = true;
                        }
                        //jesli grabarz pouzyl swojom funkcje, tak dostanie mrakoszlapa
                        if (grabarz)
                        {
                            grabarzMrakoszlaps++;
                        }
                        players[rightPlayer].cardTypes.RemoveAt(item);
                        players[rightPlayer].cardNumbers.RemoveAt(item);
                    }
                    //umrzil
                    else
                    {
                        if (players[rightPlayer].hasPijavica)
                        {
                            pijawicaMrakoszlaps++;
                        }
                        report[21] += text[123, lang] + players[rightPlayer].name + ".";
                        death(rightPlayer, -1);
                    }
                }
                drawPlayersCardsRTB();
                this.Invoke((MethodInvoker)delegate { drawPlayers(); });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void matrixShoot(int target, int from, int bulletNumber)
        {
            try
            {
                //drawing bullets
                if (flyingCheckBox.Checked)
                {
                    drawBulletShoot(players[from].position.x, players[from].position.y, players[target].position.x, players[target].position.y);
                }
                else
                {
                    drawBullet(players[from].position.x, players[from].position.y, players[target].position.x, players[target].position.y);
                }
                //neprustrzelno westa
                if (players[target].cardTypes.Contains((int)cardTypeNumber.neprustrzelnoWesta))
                {
                    int item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.neprustrzelnoWesta);
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[108, lang] + (players[target].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                    });
                    report[9 + bulletNumber] += text[109, lang] + (players[target].cardNumbers[item] + 1) + ".";
                    players[target].cardTypes.RemoveAt(item);
                    players[target].cardNumbers.RemoveAt(item);
                }
                //mrakoszlap
                else if (players[target].cardTypes.Contains((int)cardTypeNumber.mrakoszlap))
                {
                    int item = players[target].cardTypes.FindIndex(x => x == (int)cardTypeNumber.mrakoszlap);
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[target].name + text[116, lang] + (players[target].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                    });
                    report[9 + bulletNumber] += text[110, lang] + (players[target].cardNumbers[item] + 1) + ".";
                    if (players[target].hasPijavica)
                    {
                        pijawicaMrakoszlaps++;
                    }
                    //jesli to je gandalf, tak kuskona zyskuje mrakoszlap
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.gandalf) && !kuskonaZyskolMraka && players[cards[(int)cardTypeNumber.kusKona].cards[0].player].alive)
                    {
                        kuskonaZyskolMraka = true;
                    }
                    //jesli to je kuskona, tak gandalf zyskuje mrakoszlap
                    if (players[target].cardTypes.Contains((int)cardTypeNumber.kusKona) && !gandalfZyskolMraka && players[cards[(int)cardTypeNumber.gandalf].cards[0].player].alive)
                    {
                        gandalfZyskolMraka = true;
                    }
                    //jesli grabarz pouzyl swojom funkcje, tak dostanie mrakoszlapa
                    if (grabarz)
                    {
                        grabarzMrakoszlaps++;
                    }
                    players[target].cardTypes.RemoveAt(item);
                    players[target].cardNumbers.RemoveAt(item);
                }
                //umrzil
                else
                {
                    if (players[target].hasPijavica)
                    {
                        pijawicaMrakoszlaps++;
                    }
                    this.Invoke((MethodInvoker)delegate { drawPlayers(); });
                    death(target, 9 + bulletNumber);
                }
                drawPlayersCardsRTB();
                this.Invoke((MethodInvoker)delegate { drawPlayers(); });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void votedShoot(int player)
        {
            try
            {
                //voted
                if (votedShot == 0)
                {
                    //prowazochodec
                    if (players[player].cardTypes.Contains((int)cardTypeNumber.prowazochodec))
                    {
                        int item = players[player].cardTypes.FindIndex(x => x == (int)cardTypeNumber.prowazochodec);
                        int mrakoszlap = players[player].cardNumbers[item];
                        Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[124, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        InfoRTB.Text += text[125, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        players[player].cardTypes.RemoveAt(item);
                        players[player].cardNumbers.RemoveAt(item);
                    }
                    //imunita
                    else if (players[player].cardTypes.Contains((int)cardTypeNumber.imunita))
                    {
                        int item = players[player].cardTypes.FindIndex(x => x == (int)cardTypeNumber.imunita);
                        int mrakoszlap = players[player].cardNumbers[item];
                        Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[126, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        InfoRTB.Text += text[127, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        players[player].cardTypes.RemoveAt(item);
                        players[player].cardNumbers.RemoveAt(item);
                    }
                    //mrakoszlap
                    else if (players[player].cardTypes.Contains((int)cardTypeNumber.mrakoszlap))
                    {
                        int item = players[player].cardTypes.FindIndex(x => x == (int)cardTypeNumber.mrakoszlap);
                        int mrakoszlap = players[player].cardNumbers[item];
                        Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[116, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        InfoRTB.Text += text[128, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        //jesli to je gandalf, tak kuskona zyskuje mrakoszlap
                        if (players[player].cardTypes.Contains((int)cardTypeNumber.gandalf) && !kuskonaZyskolMraka && players[cards[(int)cardTypeNumber.kusKona].cards[0].player].alive)
                        {
                            kuskonaZyskolMraka = true;
                        }
                        //jesli to je kuskona, tak gandalf zyskuje mrakoszlap
                        if (players[player].cardTypes.Contains((int)cardTypeNumber.kusKona) && !gandalfZyskolMraka && players[cards[(int)cardTypeNumber.gandalf].cards[0].player].alive)
                        {
                            gandalfZyskolMraka = true;
                        }
                        players[player].cardTypes.RemoveAt(item);
                        players[player].cardNumbers.RemoveAt(item);
                    }
                    //umrzil
                    else
                    {
                        death(player, -1);
                        InfoRTB.Text += text[118, lang] + players[player].name + "." + endl; InfoLabel.Focus();
                        drawPlayers();
                    }
                }
                //shot
                else if (votedShot == 1)
                {
                    //imunita
                    if (players[player].cardTypes.Contains((int)cardTypeNumber.imunita))
                    {
                        int item = players[player].cardTypes.FindIndex(x => x == (int)cardTypeNumber.imunita);
                        Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[126, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        InfoRTB.Text += text[127, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        players[player].cardTypes.RemoveAt(item);
                        players[player].cardNumbers.RemoveAt(item);
                    }
                    //neprustrzelno westa
                    else if (players[player].cardTypes.Contains((int)cardTypeNumber.neprustrzelnoWesta))
                    {
                        int item = players[player].cardTypes.FindIndex(x => x == (int)cardTypeNumber.neprustrzelnoWesta);
                        Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[108, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        InfoRTB.Text += text[129, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        players[player].cardTypes.RemoveAt(item);
                        players[player].cardNumbers.RemoveAt(item);
                    }
                    //mrakoszlap
                    else if (players[player].cardTypes.Contains((int)cardTypeNumber.mrakoszlap))
                    {
                        int item = players[player].cardTypes.FindIndex(x => x == (int)cardTypeNumber.mrakoszlap);
                        int mrakoszlap = players[player].cardNumbers[item];
                        Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[116, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        InfoRTB.Text += text[128, lang] + (players[player].cardNumbers[item] + 1) + "." + endl; InfoLabel.Focus();
                        //jesli to je gandalf, tak kuskona zyskuje mrakoszlap
                        if (players[player].cardTypes.Contains((int)cardTypeNumber.gandalf) && !kuskonaZyskolMraka && players[cards[(int)cardTypeNumber.kusKona].cards[0].player].alive)
                        {
                            kuskonaZyskolMraka = true;
                        }
                        //jesli to je kuskona, tak gandalf zyskuje mrakoszlap
                        if (players[player].cardTypes.Contains((int)cardTypeNumber.kusKona) && !gandalfZyskolMraka && players[cards[(int)cardTypeNumber.gandalf].cards[0].player].alive)
                        {
                            gandalfZyskolMraka = true;
                        }
                        players[player].cardTypes.RemoveAt(item);
                        players[player].cardNumbers.RemoveAt(item);
                    }
                    //umrzil
                    else
                    {
                        death(player, -1);
                        InfoRTB.Text += text[118, lang] + players[player].name + "." + endl; InfoLabel.Focus();
                        drawPlayers();
                    }
                }
                drawPlayersCardsRTB();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void death(int player, int reportNumber)
        {
            try
            {
                if (reportNumber >= 0)
                {
                    report[reportNumber] += text[130, lang] + players[player].name + ".";
                }
                this.Invoke((MethodInvoker)delegate
                {
                    Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[131, lang] + endl; InfoLabel.Focus();
                });
                for (int i = 0; i < players[player].cardTypes.Count; i++)
                {
                    cards[players[player].cardTypes[i]].numInGame--;
                }
                numberOfAlivePlayers--;
                players[player].alive = false;
                players[player].cardNumbers.Clear();
                players[player].cardTypes.Clear();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        // functions for asking stuff

        public void clickPlayer(int player, string name, bool rtb2, int card)
        {
            //rtb2 is used to not write "Gracz - ... - je we więzieniu" two times
            try
            {
                this.Invoke((MethodInvoker)delegate { undoButton.Enabled = true; });
                if (!players[player].wake)
                {
                    this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[132, lang] + InfoLabel.Text; });
                    if (rtb2)
                    {
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[player].name + " - " + name + text[133, lang] + endl; InfoLabel.Focus(); });
                    }
                }
                undo = false;
                graczKíeryKliko = card;
                canClickPictureBox = true;
                clickedPlayer = -1;
                waitForClickPBset = true;
                waitForClickPB.WaitOne();
                waitForClickPBset = false;
                canClickPictureBox = false;
                if (isNight)
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        shotButton.Enabled = false;
                        shotButton.Visible = false;
                    });
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void wants(int player, string name)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate {
                    yesButton.Enabled = true;
                    noButton.Enabled = true;
                    undoButton.Enabled = true;
                });
                if (players[player].wake)
                {
                    this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[10, lang] + name + text[11, lang]; });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate { InfoLabel.Text = text[132, lang] + text[10, lang] + name + text[11, lang]; });
                    this.Invoke((MethodInvoker)delegate { Info2RTB.Text += text[8, lang] + ' ' + players[player].name + " - " + name + text[133, lang] + endl; InfoLabel.Focus(); });
                }
                undo = false;
                waitForClickYesNoset = true;
                waitForClickYesNo.WaitOne();
                waitForClickYesNoset = false;
                this.Invoke((MethodInvoker)delegate
                {
                    yesButton.Enabled = false;
                    noButton.Enabled = false;
                    if (isNight)
                    {
                        shotButton.Enabled = false;
                        shotButton.Visible = false;
                    }
                });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void wants2(string question)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate { yesButton.Enabled = true; undoButton.Enabled = true; });
                if (question != text[77, lang] && question != text[57, lang])
                {
                    this.Invoke((MethodInvoker)delegate { noButton.Enabled = true; });
                }
                this.Invoke((MethodInvoker)delegate { InfoLabel.Text = question; });
                undo = false;
                waitForClickYesNoset = true;
                waitForClickYesNo.WaitOne();
                waitForClickYesNoset = false;
                this.Invoke((MethodInvoker)delegate
                {
                    yesButton.Enabled = false;
                    noButton.Enabled = false;
                    if (isNight)
                    {
                        shotButton.Enabled = false;
                        shotButton.Visible = false;
                    }
                });
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        // functions when stuff is clicked

        private void undoButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.Invoke((MethodInvoker)delegate { undoButton.Enabled = false; });
                undo = true;
                if (gameStates.Count >= 2)
                {
                    restoreGameState(gameStates.Count - 2);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (canClickPictureBox)
                {
                    int player = pictureBoxArray[e.X, e.Y];
                    if (startPhase != 4)
                    {
                        if (addRemoveCard != 0)
                        {
                            if (addRemoveCard == 1)
                            {
                                if (player < numberOfPlayers && players[player].alive)
                                {
                                    //mrakoszlap
                                    if (addCardCombobox.SelectedIndex == 0)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[66, lang] + (nextMrakoszlap + 1) + "." + endl; InfoLabel.Focus();
                                        });
                                        players[player].cardTypes.Add((int)cardTypeNumber.mrakoszlap);
                                        players[player].cardNumbers.Add(nextMrakoszlap);
                                        cards[(int)cardTypeNumber.mrakoszlap].cards.Add(new Card(cardNames[(int)cardTypeNumber.mrakoszlap, lang], player, 1, true));
                                        cards[(int)cardTypeNumber.mrakoszlap].numInGame++;
                                        nextMrakoszlap++;
                                    }
                                    //neprustrzelno westa
                                    else if (addCardCombobox.SelectedIndex == 1)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[134, lang] + (nextKewlar + 1) + "." + endl; InfoLabel.Focus();
                                        });
                                        players[player].cardTypes.Add((int)cardTypeNumber.neprustrzelnoWesta);
                                        players[player].cardNumbers.Add(nextKewlar);
                                        cards[(int)cardTypeNumber.neprustrzelnoWesta].cards.Add(new Card(cardNames[(int)cardTypeNumber.neprustrzelnoWesta, lang], player, 1, true));
                                        cards[(int)cardTypeNumber.neprustrzelnoWesta].numInGame++;
                                        nextKewlar++;
                                    }
                                    //zwierciadlo
                                    else if (addCardCombobox.SelectedIndex == 2)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[135, lang] + (nextZwierciadlo + 1) + "." + endl; InfoLabel.Focus();
                                        });
                                        players[player].cardTypes.Add((int)cardTypeNumber.zwierciadlo);
                                        players[player].cardNumbers.Add(nextZwierciadlo);
                                        cards[(int)cardTypeNumber.zwierciadlo].cards.Add(new Card(cardNames[(int)cardTypeNumber.zwierciadlo, lang], player, 1, true));
                                        cards[(int)cardTypeNumber.zwierciadlo].numInGame++;
                                        nextZwierciadlo++;
                                    }
                                    //imunita
                                    else if (addCardCombobox.SelectedIndex == 3)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[136, lang] + (nextImunita + 1) + "." + endl; InfoLabel.Focus();
                                        });
                                        players[player].cardTypes.Add((int)cardTypeNumber.imunita);
                                        players[player].cardNumbers.Add(nextImunita);
                                        cards[(int)cardTypeNumber.imunita].cards.Add(new Card(cardNames[(int)cardTypeNumber.imunita, lang], player, 1, true));
                                        cards[(int)cardTypeNumber.imunita].numInGame++;
                                        nextImunita++;
                                    }
                                    //prowazochodec
                                    else if (addCardCombobox.SelectedIndex == 4)
                                    {
                                        this.Invoke((MethodInvoker)delegate
                                        {
                                            Info2RTB.Text += text[8, lang] + ' ' + players[player].name + text[137, lang] + (nextProwazochodec + 1) + "." + endl; InfoLabel.Focus();
                                        });
                                        players[player].cardTypes.Add((int)cardTypeNumber.prowazochodec);
                                        players[player].cardNumbers.Add(nextProwazochodec);
                                        cards[(int)cardTypeNumber.prowazochodec].cards.Add(new Card(cardNames[(int)cardTypeNumber.prowazochodec, lang], player, 1, true));
                                        cards[(int)cardTypeNumber.prowazochodec].numInGame++;
                                        nextProwazochodec++;
                                    }
                                    drawPlayersCardsRTB();
                                    yesButton.Enabled = true;
                                    noButton.Enabled = true;
                                    shotButton.Enabled = true;
                                    votedButton.Enabled = true;
                                    buttonStartNight.Enabled = true;
                                    addCardButton.Enabled = true;
                                    removeCardButton.Enabled = true;
                                    undoButton.Enabled = true;
                                    bombButton.Enabled = true;
                                    addCardCombobox.Enabled = false;
                                    addCardCombobox.Visible = false;
                                    addRemoveCard = 0;
                                    canClickPictureBox = false;
                                }
                            }
                            else
                            {
                                if (player < numberOfPlayers && players[player].alive)
                                {
                                    selectedPlayer = player;
                                    removeCardCombobox.Enabled = true;
                                    removeCardCombobox.Items.Clear();
                                    removeCardCombobox.Visible = true;
                                    for (int i = 0; i < players[player].cardNumbers.Count; i++)
                                    {
                                        removeCardCombobox.Items.Add(nameOfCard(players[player].cardTypes[i], players[player].cardNumbers[i]));
                                    }
                                    removeCardCombobox.DroppedDown = true;
                                }
                            }
                        }
                        else
                        {
                            if (isNight)
                            {
                                if (player < numberOfPlayers && players[player].alive && !(((numOfNight + 2 + posunyciDoktora) % 3 != 0) && graczKíeryKliko == (int)cardTypeNumber.doktor && players[player].cardTypes.Contains((int)cardTypeNumber.doktor)) && !(player == tunel1) && !(player == zachroniony))
                                {
                                    clickedPlayer = player;
                                    waitForClickPB.Set();
                                }
                            }
                            else
                            {
                                if (player >= 0 && player < numberOfPlayers && players[player].alive)
                                {
                                    votedShoot(player);
                                }
                                canClickPictureBox = false;
                                yesButton.Enabled = true;
                                noButton.Enabled = true;
                                shotButton.Enabled = true;
                                votedButton.Enabled = true;
                                buttonStartNight.Enabled = true;
                                addCardButton.Enabled = true;
                                removeCardButton.Enabled = true;
                                undoButton.Enabled = true;
                                bombButton.Enabled = true;
                                if (numberOfAlivePlayers == cards[(int)cardTypeNumber.mafian].numInGame || cards[(int)cardTypeNumber.mafian].numInGame == 0)
                                {
                                    waitForClickYesNo.Set();
                                }
                            }
                        }
                    }
                    else if(player < numberOfPlayers && CardsListBox.SelectedIndex >= 0 && players[player].cardNumbers.Count < (numOfAllCards / numberOfPlayers))
                    {
                        int cardType = CardsListBox.SelectedIndex;
                        int cardNumber = amountOfSpecificCards[cardType] - amountOfSpecificCardsListBox[cardType];
                        if (amountOfSpecificCardsListBox[cardType] > 0)
                        {
                            players[player].cardTypes.Add(cardType);
                            players[player].cardNumbers.Add(cardNumber);
                            cards[cardType].cards[cardNumber].player = player;
                            amountOfSpecificCardsListBox[cardType]--;
                            numOfAllCardsListBox--;
                            drawCardsListBox();
                            drawPlayersCardsRTB();
                            CardsListBox.SelectedIndex = cardType;

                            if (numOfAllCardsListBox == 0)
                            {
                                canClickPictureBox = false;
                                CardsListBox.Enabled = false;
                                //buttonStartPhase.Enabled = true;
                                //buttonStartPhase.Width = 0;
                                startPhase = 5;
                                buttonStartPhase.PerformClick();
                            }
                            else
                            {
                                while (amountOfSpecificCardsListBox[CardsListBox.SelectedIndex] == 0)
                                {
                                    CardsListBox.SelectedIndex = (CardsListBox.SelectedIndex == 0) ? 1 : ((CardsListBox.SelectedIndex + 1) % numOfSpecificCards);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (startPhase == 3)
                {
                    giveCards();
                    zapiszRozdaneKartyDoTxt();
                    drawPlayersCardsRTB();
                    startPhase = 5;
                    buttonStartPhase.PerformClick();
                }
                else
                {
                    wantsUse = true;
                    waitForClickYesNo.Set();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }
        
        private void noButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (startPhase == 3)
                {
                    startPhase = 4;
                    buttonStartPhase.PerformClick();
                }
                else
                {
                    wantsUse = false;
                    waitForClickYesNo.Set();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }
        
        private void votedButton_Click(object sender, EventArgs e)
        {
            try
            {
                yesButton.Enabled = false;
                noButton.Enabled = false;
                shotButton.Enabled = false;
                votedButton.Enabled = false;
                buttonStartNight.Enabled = false;
                addCardButton.Enabled = false;
                removeCardButton.Enabled = false;
                undoButton.Enabled = false;
                bombButton.Enabled = false;
                canClickPictureBox = true;
                votedShot = 0;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        //in day used to shoot someone, in night used to end night
        private void shotButton_Click(object sender, EventArgs e)
        {
            try
            {
                if(isNight)
                {
                    endNight = true;
                    if (waitForClickPBset)
                    {
                        waitForClickPB.Set();
                    }
                    if (waitForClickYesNoset)
                    {
                        waitForClickYesNo.Set();
                    }
                }
                else
                {
                    yesButton.Enabled = false;
                    noButton.Enabled = false;
                    shotButton.Enabled = false;
                    votedButton.Enabled = false;
                    buttonStartNight.Enabled = false;
                    addCardButton.Enabled = false;
                    removeCardButton.Enabled = false;
                    undoButton.Enabled = false;
                    bombButton.Enabled = false;
                    canClickPictureBox = true;
                    votedShot = 1;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void buttonStartNight_Click(object sender, EventArgs e)
        {
            try
            {
                wantsUse = true;
                waitForClickYesNo.Set();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void addCardButton_Click(object sender, EventArgs e)
        {
            try
            {
                yesButton.Enabled = false;
                noButton.Enabled = false;
                shotButton.Enabled = false;
                votedButton.Enabled = false;
                buttonStartNight.Enabled = false;
                addCardButton.Enabled = false;
                removeCardButton.Enabled = false;
                undoButton.Enabled = false;
                bombButton.Enabled = false;
                addCardCombobox.Enabled = true;
                addCardCombobox.Visible = true;
                addCardCombobox.DroppedDown = true;
                addRemoveCard = 1;
                canClickPictureBox = true;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void removeCardButton_Click(object sender, EventArgs e)
        {
            try
            {
                yesButton.Enabled = false;
                noButton.Enabled = false;
                shotButton.Enabled = false;
                votedButton.Enabled = false;
                buttonStartNight.Enabled = false;
                addCardButton.Enabled = false;
                removeCardButton.Enabled = false;
                undoButton.Enabled = false;
                bombButton.Enabled = false;
                addRemoveCard = 2;
                canClickPictureBox = true;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void removeCardCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int cardType = players[selectedPlayer].cardTypes[removeCardCombobox.SelectedIndex];
                int cardNumber = players[selectedPlayer].cardNumbers[removeCardCombobox.SelectedIndex];
                if (cardType == (int)cardTypeNumber.mrakoszlap ||
                    cardType == (int)cardTypeNumber.imunita ||
                    cardType == (int)cardTypeNumber.prowazochodec ||
                    cardType == (int)cardTypeNumber.neprustrzelnoWesta ||
                    cardType == (int)cardTypeNumber.slina ||
                    cardType == (int)cardTypeNumber.pijavica ||
                    cardType == (int)cardTypeNumber.zwierciadlo ||
                    cardType == (int)cardTypeNumber.terorista ||
                    cardType == (int)cardTypeNumber.meciar ||
                    cardType == (int)cardTypeNumber.kovac ||
                    cardType == (int)cardTypeNumber.alCapone ||
                    cardType == (int)cardTypeNumber.ateista ||
                    cardType == (int)cardTypeNumber.anarchista ||
                    cardType == (int)cardTypeNumber.sklenar ||
                    cardType == (int)cardTypeNumber.masowyWrah ||
                    cardType == (int)cardTypeNumber.luneta ||
                    cardType == (int)cardTypeNumber.grabarz ||
                    cardType == (int)cardTypeNumber.panCzasu ||
                    cardType == (int)cardTypeNumber.jozinZBazin)
                {
                    cards[cardType].cards[cardNumber].inGame = false;
                    players[selectedPlayer].cardTypes.RemoveAt(removeCardCombobox.SelectedIndex);
                    players[selectedPlayer].cardNumbers.RemoveAt(removeCardCombobox.SelectedIndex);
                    drawPlayersCardsRTB();
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[8, lang] + ' ' + players[selectedPlayer].name + text[138, lang] + nameOfCard(cardType, cardNumber) + "." + endl; InfoLabel.Focus();
                        drawPlayers();
                    });
                }
                else
                {
                    this.Invoke((MethodInvoker)delegate
                    {
                        Info2RTB.Text += text[155, lang] + nameOfCard(cardType, cardNumber) + text[156, lang] + ' ' + players[selectedPlayer].name + text[157, lang] + "." + endl; InfoLabel.Focus();
                    });
                }
                yesButton.Enabled = true;
                noButton.Enabled = true;
                shotButton.Enabled = true;
                votedButton.Enabled = true;
                buttonStartNight.Enabled = true;
                addCardButton.Enabled = true;
                removeCardButton.Enabled = true;
                undoButton.Enabled = true;
                bombButton.Enabled = true;
                removeCardCombobox.Items.Clear();
                removeCardCombobox.Enabled = false;
                removeCardCombobox.Visible = false;
                addRemoveCard = 0;
                canClickPictureBox = false;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void bombButton_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                votedShot = 1;
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    if (players[i].alive)
                    {
                        votedShoot(i);
                        if (numberOfAlivePlayers == cards[(int)cardTypeNumber.mafian].numInGame || cards[(int)cardTypeNumber.mafian].numInGame == 0)
                        {
                            i = numberOfPlayers;
                            waitForClickYesNo.Set();
                        }
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void speedTrackBar_Scroll(object sender, EventArgs e)
        {
            try
            {
                speedLabel.Text = text[152 + (flyingCheckBox.Checked ? 1 : 0), lang] + speedTrackBar.Value;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void flyingCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (flyingCheckBox.Checked)
                {
                    speedTrackBar.Value = 20;
                    speedTrackBar.Maximum = 50;
                    speedTrackBar.Minimum = 5;
                    speedLabel.Text = text[153, lang] + speedTrackBar.Value;
                }
                else
                {
                    speedTrackBar.Value = 0;
                    speedTrackBar.Maximum = 1000;
                    speedTrackBar.Minimum = 0;
                    speedLabel.Text = text[152, lang] + speedTrackBar.Value;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        // functions for logging stuff into txt

        public void zapiszRozdaneKartyDoTxt()
        {
            try
            {
                File.WriteAllText(".\\logs\\" + dateAndTimeWhenProgramStarted + "_RozdaneKarty.txt", String.Empty);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(".\\logs\\" + dateAndTimeWhenProgramStarted + "_RozdaneKarty.txt", true))
                {
                    foreach (string line in PlayersCardsRichTextBox.Lines)
                    {
                        file.Write(line + file.NewLine);
                    }
                    file.Close();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void zapiszCoSeDzialoDoTxt()
        {
            try
            {
                File.WriteAllText(".\\logs\\" + dateAndTimeWhenProgramStarted + "_CoSeDzialo.txt", String.Empty);
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(".\\logs\\" + dateAndTimeWhenProgramStarted + "_CoSeDzialo.txt", true))
                {
                    foreach (string line in Info2RTB.Lines)
                    {
                        file.Write(line + file.NewLine);
                    }
                    file.Close();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void zapiszErrorDoTxt(string s)
        {
            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(".\\logs\\" + dateAndTimeWhenProgramStarted + "_Errors.txt", true))
                {
                    file.Write(s);
                    file.Close();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1);
            }
        }

        // functions that help with stuff

        public string nameOfCard(int cardType, int cardNumber)
        {
            try
            {
                if (amountOfSpecificCards[cardType] == 1)
                {
                    return cardNames[cardType, lang].ToString();
                }
                else
                {
                    return (cardNames[cardType, lang] + " " + (cardNumber + 1)).ToString();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
                return "error";
            }
        }

        public void resetBullet()
        {
            try
            {
                bullet.usedMagnet = false;
                for (int i = 0; i < 3; i++)
                {
                    bullet.usedTunnel[i] = false;
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public int getLeftPlayer(int player)
        {
            try
            {
                int leftPlayer = -1;
                int p = player;
                for (int i = 0; i < numberOfPlayers && leftPlayer == -1; i++)
                {
                    p++;
                    if (p == numberOfPlayers)
                    {
                        p = 0;
                    }
                    if (players[p].alive && p != player)
                    {
                        leftPlayer = p;
                    }
                }
                return leftPlayer;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
                return -2;
            }
        }

        public int getRightPlayer(int player)
        {
            try
            {
                int rightPlayer = -1;
                int p = player;
                for (int i = 0; i < numberOfPlayers && rightPlayer == -1; i++)
                {
                    p--;
                    if (p < 0)
                    {
                        p = numberOfPlayers - 1;
                    }
                    if (players[p].alive && p != player)
                    {
                        rightPlayer = p;
                    }
                }
                return rightPlayer;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
                return -3;
            }
        }

        // functions for tunnels and drawing stuff

        public double Deg2Rad(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        public double Rad2Deg(double angle)
        {
            return 180.0 * angle / Math.PI;
        }

        public double distance(int x1, int y1, int x2, int y2)
        {
            try
            {
                return Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
                return -1;
            }
        }

        public void addTunnel(int p1, int p2)
        {
            try
            {
                bool add = true;
                //nie idzie zrobic tunel na tego samego gracza
                if (p1 == p2)
                {
                    add = false;
                }
                //nie idzie zrobic dwa razy tyn som tunel
                for (int i = 0; i < numberOfTunnels && add; i++)
                {
                    if (tunnels[i].from == p1 && tunnels[i].to == p2)
                    {
                        add = false;
                        this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[139, lang] + players[p1].name + ", " + players[p2].name + text[140, lang] + endl; InfoLabel.Focus(); });
                    }
                }
                //tunel nimoze byc zrobiony na ateiste
                if (players[p2].cardTypes.Contains(25) && add)
                {
                    add = false;
                    this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[141, lang] + players[p2].name + text[142, lang] + endl; InfoLabel.Focus(); });
                }
                if (add)
                {
                    players[p1].tunnelFrom = true;
                    Tunnel t = new Tunnel();
                    t.from = p1;
                    t.to = p2;
                    t.numOfTunnel = numberOfTunnels;
                    players[p1].tunnels.Add(t);
                    players[p1].tunnelsFrom++;
                    tunnels[numberOfTunnels].from = p1;
                    tunnels[numberOfTunnels].to = p2;
                    numberOfTunnels++;
                    this.Invoke((MethodInvoker)delegate { Info2RTB.Text += ">>> " + text[143, lang] + players[p1].name + text[91, lang] + players[p2].name + "." + endl; InfoLabel.Focus(); });
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawTunnel(int from, int to, int tunnel)
        {
            try
            {
                Pen pen = new Pen(Brushes.White);
                pen.Width = 16;
                if (tunnel == 0)
                {
                    pen.Color = Color.LightSkyBlue;
                }
                else if (tunnel == 1)
                {
                    pen.Color = Color.LightGreen;
                }
                else if (tunnel == 2)
                {
                    pen.Color = Color.LightSalmon;
                }
                
                float x1 = players[from].position.x;
                float y1 = players[from].position.y;
                float x2 = players[to].position.x;
                float y2 = players[to].position.y;
                //g.DrawLine(pen, x1, y1, x2, y2);

                double distx = x2 - x1;
                double disty = y1 - y2;
                double angle = Rad2Deg(Math.Atan2(disty, distx));
                pen.Width = 2;
                for (int i = 1; i < 20; i++)
                {
                    float nx = x1 + (x2 - x1) * i / 20;
                    float ny = y1 + (y2 - y1) * i / 20;
                    int x3 = Convert.ToInt32(nx + 15 * Math.Cos(Deg2Rad(angle + 145)));
                    int y3 = Convert.ToInt32(ny - 15 * Math.Sin(Deg2Rad(angle + 145)));
                    int x4 = Convert.ToInt32(nx + 15 * Math.Cos(Deg2Rad(angle - 145)));
                    int y4 = Convert.ToInt32(ny - 15 * Math.Sin(Deg2Rad(angle - 145)));
                    g.DrawLine(pen, nx, ny, x3, y3);
                    g.DrawLine(pen, nx, ny, x4, y4);
                }
                pen.Dispose();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawTunnels()
        {
            try
            {
                for (int i = 0; i < numberOfTunnels; i++)
                {
                    if (tunnels[i].from != -1 && players[tunnels[i].from].alive && players[tunnels[i].to].alive)
                    {
                        drawTunnel(tunnels[i].from, tunnels[i].to, i);
                    }
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawBulletShoot(float x1, float y1, float x2, float y2)
        {
            try
            {
                float speed = 20F;
                this.Invoke((MethodInvoker)delegate
                {
                    speed = speedTrackBar.Value;
                });
                vector2D pos = new vector2D(x1, y1);
                vector2D vel = new vector2D(x2 - x1, y2 - y1);
                int time = (int)Math.Ceiling(vel.mag() / speed);
                vel.setMag(speed);
                int radius = 5;
                for (int i = 0; i < time; i++)
                {
                    drawPlayers();
                    g.FillEllipse(Brushes.Black, pos.x - radius, pos.y - radius, radius * 2, radius * 2);
                    this.Invoke((MethodInvoker)delegate
                    {
                        pictureBox1.Image = bmp;
                        pictureBox1.Update();
                    });
                    pos.add(vel);
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString()); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawBullet(float x1, float y1, float x2, float y2)
        {
            try
            {
                Pen pen = new Pen(Brushes.Black);
                pen.Width = 2;

                double distx = x2 - x1;
                double disty = y1 - y2;

                g.DrawLine(pen, x1, y1, x2, y2);

                double angle = Rad2Deg(Math.Atan2(disty, distx));

                int x3 = Convert.ToInt32(x2 + 10 * Math.Cos(Deg2Rad(angle + 150)));
                int y3 = Convert.ToInt32(y2 - 10 * Math.Sin(Deg2Rad(angle + 150)));
                int x4 = Convert.ToInt32(x2 + 10 * Math.Cos(Deg2Rad(angle - 150)));
                int y4 = Convert.ToInt32(y2 - 10 * Math.Sin(Deg2Rad(angle - 150)));

                g.DrawLine(pen, x2, y2, x3, y3);
                g.DrawLine(pen, x2, y2, x4, y4);

                this.Invoke((MethodInvoker)delegate {
                    pictureBox1.Image = bmp;
                    pictureBox1.Update();
                    Thread.Sleep(speedTrackBar.Value);
                });

                pen.Dispose();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        public void drawPlayers()
        {
            try
            {
                g.Clear(Color.White);
                drawTunnels();
                for (int i = 0; i < numberOfPlayers; i++)
                {
                    if (players[i].alive)
                    {
                        g.DrawEllipse(Pens.Black, players[i].position.x - circleDiameter, players[i].position.y - circleDiameter, circleDiameter * 2, circleDiameter * 2);
                        g.DrawString(players[i].name, font, Brushes.Black, players[i].position.x - 4 * (players[i].name.Length), players[i].position.y - 8);
                        if (isNight)
                        {
                            if (players[i].hasSlina)
                            {
                                g.DrawImage(slinaImage, players[i].position.x, players[i].position.y, 50, 50);
                            }
                            if (players[i].hasPiosek)
                            {
                                g.DrawImage(piosekImage, players[i].position.x, players[i].position.y - 60, 50, 40);
                            }
                            if (players[i].hasMagnet)
                            {
                                g.DrawImage(magnetImage, players[i].position.x - 60, players[i].position.y + 7, 45, 40);
                            }
                            if (matrix && players[i].cardTypes.Contains((int)cardTypeNumber.matrix))
                            {
                                g.DrawImage(matrixImage, players[i].position.x - 70, players[i].position.y - 50, 70, 40);
                            }
                        }
                        else
                        {
                            if (players[i].cardTypes.Contains((int)cardTypeNumber.meciar))
                            {
                                g.DrawImage(meciarImage, players[i].position.x + 20, players[i].position.y, 50, 50);
                            }
                            if (players[i].cardTypes.Contains((int)cardTypeNumber.kovac))
                            {
                                g.DrawImage(kovacImage, players[i].position.x + 20, players[i].position.y - 60, 50, 50);
                            }
                            if (players[i].cardTypes.Contains((int)cardTypeNumber.masowyWrah))
                            {
                                g.DrawImage(masovyVrahImage, players[i].position.x - 70, players[i].position.y, 50, 50);
                            }
                            if (players[i].hasZakazGlosowanio)
                            {
                                g.DrawImage(soudceImage, players[i].position.x - 70, players[i].position.y - 60, 50, 50);
                            }
                            if (players[i].ofiaraKata)
                            {
                                g.DrawImage(ofiaraImage, players[i].position.x - 17, players[i].position.y - 68, 40, 60);
                            }
                            if (players[i].zachronionyKatym)
                            {
                                g.DrawImage(zachronionyImage, players[i].position.x - 17, players[i].position.y + 8, 40, 60);
                            }
                        }
                    }
                }
                pictureBox1.Image = bmp;
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        // functions not even worth describing

        private void InfoRTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                InfoRTB.SelectionStart = InfoRTB.Text.Length;
                InfoRTB.ScrollToCaret();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }
        
        private void Info2RTB_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Info2RTB.SelectionStart = Info2RTB.Text.Length;
                Info2RTB.ScrollToCaret();
            }
            catch (Exception exception1)
            {
                MessageBox.Show("An error occurred:\n" + exception1); zapiszErrorDoTxt(exception1.ToString());
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            thread2.Abort();
            Thread.Sleep(50);
        }
    }
}
