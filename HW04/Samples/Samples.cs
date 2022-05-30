using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW04
{
    public static class Samples
    {
        public static string StringSample() => @"VŠECHNO, CO OPRAVDU POTŘEBUJU ZNÁT o tom, jak žít, co dělat a jak vůbec být, jsem se naučil v mateřské školce. Moudrost mě nečekala na vrcholu hory zvané postgraduál, ale na pískovišti v nedělní škole. Tohle jsem se tam naučil:
    O všechno se rozděl.
    Hraj fér.
    Nikoho nebij.
    Vracej věci tam, kde jsi je našel.
    Uklízej po sobě.
    Neber si nic, co ti nepatří.
    Když někomu ublížíš, řekni promiň.
    Před jídlem si umyj ruce.
    Splachuj.
    Teplé koláčky a studené mléko ti udělají dobře.
    Žij vyrovnaně - trochu se uč a trochu přemýšlej a každý den trochu maluj a kresli a tancuj a hraj si a pracuj.
    Každý den odpoledne si zdřímni.
    Když vyrazíš do světa, dávej pozor na auta, chytni někoho za ruku a drž se s ostatními pohromadě.
    Nepřestávej žasnout. Vzpomeň si na semínko v plastikovém kelímku - kořínky míří dolů a rostlinka stoupá vzhůru a nikdo vlastně neví jak a proč, ale my všichni jsme takoví.
    Zlaté rybičky, křečci a bílé myšky a dokonce i to semínko v kelímku - všichni umřou. My také.
    A nikdy nezapomeň na dětské obrázkové knížky a první slovo, které ses naučil - největší slovo ze všech - DÍVEJ SE.";

        public static async Task<Image> ImageSample() => await Image.LoadAsync("Samples/stabbyCat.png");
    }
}
