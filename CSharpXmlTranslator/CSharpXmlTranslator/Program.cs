using CSharpXmlTranslator;

Course course = new Course();
course.Title = "Asp.Net";
course.Fees = 30000;
course.Teacher = new Instructor()
{
    Name = "Jalaluddin",
    Email = "jalauddin123@gmail.com",
    PresentAddress = new Address
    {
        Street = "3",
        City = "Dhaka",
        Country = "Bangladesh"
    },
    PermanentAddress = new Address
    {
        Street = "5",
        City = null,
        Country = "Bangladesh"
    },
    PhoneNumbers = new List<Phone> {
        new Phone {Number = "123456",CountryCode="091",Extension="+330"},
        new Phone {Number = "45689",CountryCode="0918",Extension="+390"},
        new Phone {Number = "123456",CountryCode="09156",Extension="+3560"},
    },
    // etc.
};
// all other fields set here.
course.Topics = new List<Topic>() {
    new Topic{Title= "Intro",Description= "Get to Know about Course",
        Sessions = new List<Session>{
        new Session{DurationInHour = 1,LearningObjective= "Overall Idea" },
        } },
    new Topic{Title= "Delegate",Description= "Advantages of deligates",
        Sessions = new List<Session>{
        new Session{DurationInHour = 2,LearningObjective= "Overall Idea about delegates" },
        } },
    new Topic{Title= "Events",Description= "Delegate Alternatives",
        Sessions = new List<Session>{
        new Session{DurationInHour = 1,LearningObjective= "Overall Idea about uses of Func and Action" },
        } },
};
course.Tests = new List<AdmissionTest> {
new AdmissionTest{StartDateTime = new DateTime(2023,1,28,11,5,55),
    EndDateTime=  new DateTime(2023,1,28,12,5,55),
    TestFees = 100.00
},
new AdmissionTest{StartDateTime = new DateTime(2023,1,29,11,5,55),
    EndDateTime=  new DateTime(2023,1,29,12,5,55),
    TestFees = 100.00
}
};

string xml = XMLFormatter.Convert(course);
Console.WriteLine(xml);
