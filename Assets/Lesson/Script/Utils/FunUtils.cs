using UnityEngine;
namespace StringUtils
{
    public class FunUtils : MonoBehaviour
    {
        public static string RandomName()
        {
            string[] firstNames = {
                "Emma", "Liam", "Olivia", "Noah", "Ava", "Isabella", "Sophia", "Jackson", "Lucas", "Aiden",
                "Elijah", "Mia", "Carter", "Evelyn", "Abigail", "Harper", "Grace", "Scarlett", "Chloe", "Lily",
                "Layla", "Amelia", "Benjamin", "Mason", "Ella", "Aria", "Riley", "Zoe", "Hannah", "Samuel",
                "Nora", "Leo", "Lillian", "Jackson", "David", "Penelope", "Avery", "Ezra", "Victoria", "Madison",
                "Eleanor", "Mateo", "Julian", "Adeline", "Hazel", "Ellie", "Nova", "Asher", "Skylar", "Allison",
                "Sofia", "Ruby", "Scarlett", "Joseph", "Gabriel", "Naomi", "Caleb", "Jaxon", "Emilia", "Levi",
                "Lucy", "Gianna", "Lincoln", "Nicholas", "Kai", "Madelyn", "Bella", "Zachary", "Jasmine", "Xavier",
                "Paisley", "Tyler", "Autumn", "William", "Grace", "Chase", "Natalie", "Aurora", "Mila", "Elena",
                "Eli", "Camila", "Isaac", "Aaliyah", "Connor", "Eva", "Aiden", "Audrey", "Samantha", "Zayden",
                "Ariana", "Adam", "Sadie", "Lydia", "Alexa"
            };

            string[] lastNames = {
                "Smith", "Johnson", "Williams", "Jones", "Brown", "Davis", "Miller", "Wilson", "Moore", "Taylor",
                "Anderson", "Thomas", "Jackson", "White", "Harris", "Martin", "Thompson", "Garcia", "Martinez", "Robinson",
                "Clark", "Rodriguez", "Lewis", "Lee", "Walker", "Hall", "Allen", "Young", "Hernandez", "King",
                "Wright", "Lopez", "Hill", "Scott", "Green", "Adams", "Baker", "Gonzalez", "Nelson", "Carter",
                "Mitchell", "Perez", "Roberts", "Turner", "Phillips", "Campbell", "Parker", "Evans", "Edwards", "Collins",
                "Stewart", "Sanchez", "Morris", "Rogers", "Reed", "Cook", "Morgan", "Bell", "Murphy", "Bailey",
                "Rivera", "Cooper", "Richardson", "Cox", "Howard", "Ward", "Torres", "Peterson", "Gray", "Ramirez",
                "James", "Watson", "Brooks", "Kelly", "Sanders", "Price", "Bennett", "Wood", "Barnes", "Ross",
                "Henderson", "Coleman", "Jenkins", "Perry", "Powell", "Long", "Patterson", "Hughes", "Flores", "Washington"
            };


            int randomFirstNameIndex = Random.Range(0, firstNames.Length);
            int randomLastNameIndex = Random.Range(0, lastNames.Length);

            string randomName = $"{firstNames[randomFirstNameIndex]} {lastNames[randomLastNameIndex]}";

            return randomName;
        }


    }
}