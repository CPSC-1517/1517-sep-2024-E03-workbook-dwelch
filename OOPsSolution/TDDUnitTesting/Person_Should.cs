using FluentAssertions;
using OOPsReview;

namespace TDDUnitTesting
{
    public class Person_Should
    {
        #region Constructors

        //valid data

        //a Fact unit test executes once
        //without the annotation the method is NOT considered a unit test
        [Fact]
        public void Create_Instance_Using_Default_Constructor()
        {
            //Arrange (setup the needed code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;

            //Act (this is the action that is under testing)
            //sut: subject under test
            Person sut = new Person();

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
        }

        [Fact]
        public void Create_Instance_Using_Greedy_Constructor_Without_Address_And_Employments()
        {
            //Arrange (setup the needed code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;

            //Act (this is the action that is under testing)
            //sut: subject under test
            Person sut = new Person(expectedFirstName, expectedLastName, null, null);

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().BeNull();
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
        }

        [Fact]
        public void Create_Instance_Using_Greedy_Constructor_With_Address_And_Without_Employments()
        {
            //Arrange (setup the needed code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 0;
            ResidentAddress expectedAddress = new ResidentAddress(12, "Maple St.", "Edmonton",
                                                                    "AB","T6Y7U8");

            //Act (this is the action that is under testing)
            //sut: subject under test
            Person sut = new Person(expectedFirstName, expectedLastName, expectedAddress, null);

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
        }
        [Fact]
        public void Create_Instance_Using_Greedy_Constructor_With_Address_And_Employments()
        {
            //Arrange (setup the needed code for doing the test)
            string expectedFirstName = "Unknown";
            string expectedLastName = "Unknown";
            int expectedEmploymentPositionCount = 2;
            ResidentAddress expectedAddress = new ResidentAddress(12, "Maple St.", "Edmonton",
                                                                    "AB", "T6Y7U8");
            //how to test a collection?
            //create individual instances of the item in the list
            //it this example those instances are objects
            //you must remember each object has a unique GUID
            //NOTE: you CANNOT reuse a single variable to hold the separate instances
            Employment one = new Employment("Team Member", SupervisoryLevel.TeamMember,
                                                DateTime.Parse("2013/10/23"), 6.5);
            Employment two = new Employment("Team Lead", SupervisoryLevel.TeamLeader,
                                                DateTime.Parse("2020/04/13"), 4.4);
            List<Employment> expectedEmployments = new List<Employment>();
            expectedEmployments.Add(one);
            expectedEmployments.Add(two);

            //Act (this is the action that is under testing)
            //sut: subject under test
            Person sut = new Person(expectedFirstName, expectedLastName, expectedAddress, expectedEmployments);

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
            sut.LastName.Should().Be(expectedLastName);
            sut.Address.Should().Be(expectedAddress);
            sut.EmploymentPositions.Count().Should().Be(expectedEmploymentPositionCount);
            //did the greedy constructor actually use the data I submitted
            //were the instances in the list loaded as expected
            //check the actual contents of the list
            sut.EmploymentPositions.Should().ContainInConsecutiveOrder(expectedEmployments);

        }

        //invalid data
        //a [Theory] test will execute once for each [InlineData] notation
        //[InlineData] holds the test value for the iteration of the test
        //the method needs a parameter to receive the data from the [InlineData] annotation
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Creating_Instance_Using_Greedy_Constructor_With_Bad_First_Name(string firstname)
        {
            //Arrange (setup the needed code for doing the test)
            //Since there should be NO resulting instance created, there would be NO expected results

            //Act (this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown
            Action action = () => new Person(firstname, "Behold", null, null);

            //Assert (check the results of the act against expected action)
            action.Should().Throw<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Creating_Instance_Using_Greedy_Constructor_With_Bad_Last_Name(string lastname)
        {
            //Arrange (setup the needed code for doing the test)
            //Since there should be NO resulting instance created, there would be NO expected results

            //Act (this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown
            Action action = () => new Person("Lowand", lastname, null, null);

            //Assert (check the results of the act against expected action)
            action.Should().Throw<ArgumentNullException>();
        }
        #endregion

        #region Properties

        //valid data
        //test to directly change the First Name property
        [Fact]
        public void Directly_Change_First_Name_Via_Property()
        {
            //Arrange (setup the needed code for doing the test)
            string expectedFirstName = "Bob";
            //since we are changing an existing value in the instance,
            //  one needs the instance in the first place
            Person sut = new Person("don", "welch", null, null);

            //Act (this is the action that is under testing)
            //image the following statement is taken from an programmer's code
            sut.FirstName = "Bob";

            //Assert (check the results of the act against expected values)
            sut.FirstName.Should().Be(expectedFirstName);
        }
        [Fact]
        public void Directly_Change_Last_Name_Via_Property()
        {
            //Arrange (setup the needed code for doing the test)
            string expectedLastName = "Bob";
            //since we are changing an existing value in the instance,
            //  one needs the instance in the first place
            Person sut = new Person("don", "welch", null, null);

            //Act (this is the action that is under testing)
            //image the following statement is taken from an programmer's code
            sut.LastName = "Bob";

            //Assert (check the results of the act against expected values)
            sut.LastName.Should().Be(expectedLastName);
        }

        //invalid data
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Throw_Exception_Changing_FistName_With_Bad_First_Name(string firstname)
        {
            //Arrange (setup the needed code for doing the test)
            Person sut = new Person("don", "welch", null, null);

            //Act (this is the action that is under testing)
            //the act in this case is the capture of the exception that has been thrown
            Action action = () => sut.FirstName = firstname;

            //Assert (check the results of the act against expected action)
            action.Should().Throw<ArgumentNullException>();
        }

        #endregion

        #region Methods
        #endregion
    }
}