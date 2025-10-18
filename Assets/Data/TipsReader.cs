using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace Data
{
    public class TipsReader
    {
        private const string Json = @"
        {
            ""tips"":  [
            {
                ""id"": 1,
                ""title"": ""Adotta una dieta più sana e sostenibile"",
                ""description"": ""La vita è frenetica e preparare pasti nutrienti può essere una sfida, ma i pasti sani non devono essere elaborati. Internet è pieno di ricette rapide e salutari che puoi condividere con la tua famiglia e i tuoi amici.""
            },
            {
                ""id"": 2,
                ""title"": ""Compra solo ciò di cui hai bisogno"",
                ""description"": ""Pianifica i tuoi pasti. Fai una lista della spesa e attieniti ad essa, evita gli acquisti impulsivi. Non solo sprecherai meno cibo, ma risparmierai anche denaro!""
            },
            {
                ""id"": 3,
                ""title"": ""Scegli frutta e verdura brutte"",
                ""description"": ""Non giudicare il cibo dalla sua apparenza! Frutta e verdura dalla forma strana o ammaccata vengono spesso gettate perché non rispettano arbitrari standard estetici. Non preoccuparti: hanno lo stesso sapore! Usa la frutta matura per frullati, succhi e dolci.""
            },
            {
                ""id"": 4,
                ""title"": ""Conserva il cibo saggiamente"",
                ""description"": ""Sposta i prodotti più vecchi davanti nel tuo dispensario o frigorifero e quelli nuovi dietro. Utilizza contenitori ermetici per mantenere fresco il cibo aperto in frigorifero e assicurati che i pacchi siano chiusi per evitare che gli insetti entrino.""
            },
            {
                ""id"": 5,
                ""title"": ""Comprendi l'etichettatura alimentare"",
                ""description"": ""C'è una grande differenza tra le date 'preferibile entro' e 'da consumarsi entro'. A volte il cibo è ancora sicuro da mangiare dopo la data 'preferibile entro', mentre è la data 'da consumarsi entro' che indica quando non è più sicuro. Controlla le etichette alimentari per ingredienti non salutari come grassi trans e conservanti e evita cibi con zucchero o sale aggiunto.""
            },
            {
                ""id"": 6,
                ""title"": ""Inizia piano"",
                ""description"": ""Prendi porzioni più piccole a casa o condividi piatti abbondanti nei ristoranti.""
            },
            {
                ""id"": 7,
                ""title"": ""Ama gli avanzi"",
                ""description"": ""Se non mangi tutto ciò che prepari, congela per dopo o usa gli avanzi come ingrediente per un altro pasto.""
            },
            {
                ""id"": 8,
                ""title"": ""Dai un utilizzo agli scarti alimentari"",
                ""description"": ""Invece di gettare i resti alimentari, componili. In questo modo stai restituendo nutrienti al suolo e riducendo la tua impronta di carbonio.""
            },
            {
                ""id"": 9,
                ""title"": ""Rispetta il cibo"",
                ""description"": ""Il cibo ci connette tutti. Riconnettiti con il cibo conoscendo il processo che va nel prepararlo. Leggi sulla produzione alimentare e conosci i tuoi agricoltori.""
            },
            {
                ""id"": 10,
                ""title"": ""Supporta i produttori locali di cibo"",
                ""description"": ""Acquistando prodotti locali, supporti agricoltori familiari e piccole imprese nella tua comunità. Aiuti anche a combattere l'inquinamento riducendo le distanze di consegna per camion e altri veicoli.""
            },
            {
                ""id"": 11,
                ""title"": ""Mantieni le popolazioni di pesci sostenibili"",
                ""description"": ""Mangia specie di pesce più abbondanti, come sgombro o aringa, anziché quelle a rischio di sovrapesca, come merluzzo o tonno. Acquista pesce pescato o allevato in modo sostenibile, come il pesce con etichetta ecologica o certificato.""
            },
            {
                ""id"": 12,
                ""title"": ""Usa meno acqua"",
                ""description"": ""Non possiamo produrre cibo senza acqua! Mentre è importante che gli agricoltori utilizzino meno acqua per coltivare il cibo, ridurre gli sprechi alimentari salva anche tutte le risorse idriche impiegate nella produzione. Riduci anche il tuo consumo di acqua in altri modi: ripara le perdite o spegni l'acqua mentre ti lavi i denti!""
            },
            {
                ""id"": 13,
                ""title"": ""Mantieni pulite le nostre terre e acque"",
                ""description"": ""Alcuni rifiuti domestici sono potenzialmente pericolosi e non dovrebbero mai essere gettati in un normale bidone della spazzatura. Articoli come batterie, vernici, telefoni cellulari, medicinali, prodotti chimici, fertilizzanti, pneumatici, cartucce d'inchiostro, ecc. possono infiltrarsi nei nostri terreni e nell'approvvigionamento idrico, danneggiando le risorse naturali che producono il nostro cibo.""
            },
            {
                ""id"": 14,
                ""title"": ""Mangia più legumi e verdure"",
                ""description"": ""Una volta alla settimana, prova a mangiare un pasto basato su legumi o cereali 'antichi' come la quinoa.""
            },
            {
                ""id"": 15,
                ""title"": ""Condividi è amore"",
                ""description"": ""Dona il cibo che altrimenti verrebbe sprecato. Ad esempio, le app possono mettere in contatto i vicini tra di loro e con le attività locali in modo che il cibo in eccesso possa essere condiviso, non buttato via.""
            }
        ]
        }";

        public static TipsCollection GetTips()
        {
            TipsCollection tipsCollection = JsonConvert.DeserializeObject<TipsCollection>(Json);

            // Now you can access the tips as a list of Tip objects
            return tipsCollection;
        }
        
        public class Tip
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
        }

        public class TipsCollection
        {
            public List<Tip> Tips { get; set; }
            
            public Tip GetById(int id)
            {
                return Tips.Find(t => t.Id == id);
            }

            public Tip GetRandomTip()
            {
                var id = Random.Range(0, Tips.Count);
                
                return Tips[id];
            }
        }

    }
}
