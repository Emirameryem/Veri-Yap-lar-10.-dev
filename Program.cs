using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VeriYapilariOdev12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Ağaç Veri Yapısı - Menü Sistemi");
            Tree bst = new Tree();
            bool devam = true;

            while (devam)
            {
                Console.WriteLine("\nYapmak istediğiniz işlemi seçin:");
                Console.WriteLine("1. Ekleme");
                Console.WriteLine("2. Silme");
                Console.WriteLine("3. Arama");
                Console.WriteLine("4. PreOrder Gezinti");
                Console.WriteLine("5. InOrder Gezinti");
                Console.WriteLine("6. PostOrder Gezinti");
                Console.WriteLine("7. Çıkış");

                Console.Write("Seçiminiz: ");
                int secim = int.Parse(Console.ReadLine());

                switch (secim)
                {
                    case 1: // Ekleme
                        Console.Write("Eklemek istediğiniz değeri girin: ");
                        int eklenecek = int.Parse(Console.ReadLine());
                        bst.root = bst.insert(bst.root, eklenecek);
                        Console.WriteLine($"Değer {eklenecek} başarıyla eklendi.");
                        break;

                    case 2: // Silme
                        Console.Write("Silmek istediğiniz değeri girin: ");
                        int silinecek = int.Parse(Console.ReadLine());
                        bst.Sil(silinecek);
                        Console.WriteLine($"Değer {silinecek} silindi (eğer mevcutsa).");
                        break;

                    case 3: // Arama
                        Console.Write("Aramak istediğiniz değeri girin: ");
                        int aranacak = int.Parse(Console.ReadLine());
                        bst.find(bst.root, aranacak);
                        break;

                    case 4: // PreOrder
                        Console.WriteLine("PreOrder Gezinti:");
                        bst.preOrder(bst.root);
                        Console.WriteLine();
                        break;

                    case 5: // InOrder
                        Console.WriteLine("InOrder Gezinti:");
                        bst.InOrder(bst.root);
                        Console.WriteLine();
                        break;

                    case 6: // PostOrder
                        Console.WriteLine("PostOrder Gezinti:");
                        bst.postOrder(bst.root);
                        Console.WriteLine();
                        break;

                    case 7: // Çıkış
                        devam = false;
                        Console.WriteLine("Programdan çıkılıyor...");
                        break;

                    default:
                        Console.WriteLine("Hatalı seçim yaptınız. Lütfen tekrar deneyin.");
                        break;
                }
            }
        }
    }

    class Node // düğüm yapısını oluşturuyoruz
    {
        public int data;
        public Node Sol;
        public Node Sag;

        public Node(int data)
        {
            this.data = data;
            Sol= null;
            Sag= null;
        }
    }

    class Tree
    {
        public Node kök;
        public Node root; // ağacın başını yani kökünü tutan elemandır

        public Tree()
        {
            root=null;  
        }

        public Node newNode(int data) // ağacın rootunu oluşturan metod
        {
            root = new Node(data);
            return root;
        }

        public Node insert(Node root, int data) // ağaca ekleme yapan metod
        {
            Node eleman = new Node(data);

            if (root!= null) // kök boş değilse 
            {
                if (data < root.data) // parametre olarak gelen data roottan küçükse sola ekle
                {
                    root.Sol = insert(root.Sol, data);
                }
                else // parametre olarak gelen data roottan büyükse sağa ekle
                {
                    root.Sag = insert(root.Sag, data);
                }
            }
            else // eğer kök boşsa parametre olarak girilen datayı kök yap
            {
                root = newNode(data);
            }
            
            return root; // rootu tekrar döndür
        }

        public void preOrder(Node root) // önce kök sonra sol dal sonra sağ dalda dolaşma
        {

            if (root != null) // kök boş değilse 
            {
                Console.Write(root.data+" "); // kökü yazdır
                preOrder(root.Sol); // kökün soluna git
                preOrder(root.Sag); // kökün sağına git
            }
        }

        public void InOrder(Node root) // önce sol dal sonra kök sonra sağ dalda dolaşma
        {

            if (root != null) // kök boş değilse 
            {
                InOrder(root.Sol); // kökün soluna git
                Console.Write(root.data+" "); // kökü yazdır
                InOrder(root.Sag); // kökün sağına git

            }
        }

        public void postOrder(Node root) // önce sol dal sonra sağ dalda sonra kökte dolaşma
        {

            if (root != null) // kök boş değilse 
            {
                postOrder(root.Sol); // kökün soluna git
                postOrder(root.Sag); // kökün sağına git
                Console.Write(root.data + " "); // kökü yazdır

            }
        }


        public Node find(Node root, int data) // ağaçta arama yapma
        {

            if(root != null)
            {
                if (data == root.data)
                {
                    Console.WriteLine("sayı bulundu");
                }
                else if(data < root.data)
                {
                    find(root.Sol, data); // sayı küçükse solt alt ağaçta ara
                }
                else
                {
                    find(root.Sag, data); // sayı büyükse sağ alt ağaçta ara
                }
            }
            else
            {
                Console.WriteLine("sayı bulunamadı");
            }
            return root; 
            

        }

        public void Sil(int data)
        {
            kök = Sil(kök, data);
        }

        private Node Sil(Node kök, int data)
        {
            if (kök == null)
                return kök;


            if (data < kök.data) // silinmesi istenen sayı kökten küçükse sola git
                kök.Sol = Sil(kök.Sol, data);

            else if (data > kök.data) // silinmesi istenen sayı kökten küçükse sağa git
                kök.Sag = Sil(kök.Sag, data);

            else
            {
                // 1. Durum: Yaprak düğüm
                if (kök.Sol == null && kök.Sag == null) // kökün çoçukları yoksa
                    return null;

                // 2. Durum: Tek çocuklu düğüm
                else if (kök.Sol == null)
                    return kök.Sag;
                else if (kök.Sag == null)
                    return kök.Sol;

                // 3. Durum: İki çocuklu düğüm
                kök.data = MinDeğer(kök.Sag);
                kök.Sag = Sil(kök.Sag, kök.data);
            }

            return kök;
        }

        private int MinDeğer(Node node)
        {
            int min = node.data;
            while (node.Sol != null)
            {
                min = node.Sol.data;
                node = node.Sol;
            }
            return min;
        }
    }
}
