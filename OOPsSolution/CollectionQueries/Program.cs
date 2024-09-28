// See https://aka.ms/new-console-template for more information

using OOPsReview;

Console.WriteLine("\n\tCollection Queries\n");

List<Employment> employments = CreateCollection();

//you could use the datatype var instead of the strong datatype Employment
//using var: the datatype is resolved at run time
//using Employment: the datatype is resolved at compile time
foreach(Employment item in employments)
{
    Console.WriteLine($"{item}");
}

//locate an item in the collection
//sequencial search

//variable to hld the found item
Employment foundEmployment = null;

//iterate through the collection using a for loop
//for(int i = 0; i < employments.Count; i++)
//{
//    if (employments[i].Title.Equals("PG II"))
//    {
//        foundEmployment = employments[i];
//        //at this point one could exit the loop
//        //do properly using structure code, the exit is
//        i = employments.Count;
//    }
//}

//iterate through the collection using a foreach loop
//this is a pre-test loop
//traverse your collection from the start to the end
//  WITHOUT your need to handle the index
//it uses a placeholder to hang onto the instance of the collection
//  that is being examined on the iteration
foreach(var item in employments)
{
    if (item.Title.Equals("PG II"))
    {
        foundEmployment = item;
        //there is no quick exit using foreach
        //  that is structured
    }
}

//determine the result of the search
TestForFoundItem(foundEmployment, "PG II");

//Are there easier ways of doing a search without having to code my own? Yes:

//collections such as List<T> have methods already created for basic functionality
//one such method is .Find(predicate)
//a predicate is the condition you are using as if it was in a loop
//the predicate uses the lamda symbol (=>) in it's grammar
//  syntax:  placeholder => condition(S)

//what are collections: Array, List<T>, IEnumerable, IQueryable
//search for an item of PG II in the collection employments
string searcharg = "PG";
foundEmployment = null; //reuse of variable
//search
//.Equals() is an exact match
foundEmployment = employments.Find(e => e.Title.Equals(searcharg));
//found test
TestForFoundItem(foundEmployment, searcharg);

//.Contains(condition) looks for the condition somewhere within your data
foundEmployment = employments.Find(e => e.Title.Contains(searcharg));
TestForFoundItem(foundEmployment, searcharg);

//wonder if you don't need the actual data value
//is there a searching method to just say, Yes or No
//the .Any and .All return a boolean result if the instance is found (true) or not (false)
//.Any(condition) looks for at least one
//.All(condition) looks for all instance in the collection to match
SupervisoryLevel argment = SupervisoryLevel.DepartmentHead;
if (employments.Any(e => e.Level == argment))
{
    Console.WriteLine($"\nEmployee was once a {argment}");
}
else
{
    Console.WriteLine($"\nEmployee was never a {argment}");
}

//there is another set of commands that can be used that look very
//  similar to sql query commands and clauses
//this software is called Linq
//there are 2 styles in coding linq commands:
// query syntax (similar to sql clauses)
// method syntax (method names are similar to the collection methods)

//In Linq .Find can be replaced with .Where
//.Where will return by default a collection of all instances matching the condition
//the default for Linq for collections is IEnumerable or IQueryable
//problem: the expected return datatype is List<T>, the actual return datatype is IEnumerable
//solution: Linq has methods to do conversions:method ToList();
Console.WriteLine("\n\nLinq results\n");
List<Employment> foundCollection = null;
foundCollection = employments.Where(e => e.Title.Contains(searcharg)).ToList();
foreach(var item in foundCollection)
{
    Console.WriteLine($"\n An employment of {searcharg} was found: {item}");
}

//support methods

static List<Employment> CreateCollection()
{
    List<Employment> newCollection = new List<Employment>();

    newCollection.Add(new Employment("PG I", SupervisoryLevel.Entry,
                                        DateTime.Parse("May 1, 2010"), 0.5));
    newCollection.Add(new Employment("PG II", SupervisoryLevel.TeamMember,
                                    DateTime.Parse("Nov 1, 2010"), 3.2));
    newCollection.Add(new Employment("PG III", SupervisoryLevel.TeamLeader,
                                    DateTime.Parse("Jan 6, 2014"), 8.6));
    newCollection.Add(new Employment("SP I", SupervisoryLevel.Supervisor,
                                    DateTime.Parse("Jul 22, 2022"), 2.5));

    return newCollection;
}

static void TestForFoundItem(Employment foundEmployment, string searcharg)
{
    if (foundEmployment == null)
    {
        Console.WriteLine($"\nPerson had no {searcharg} employment");
    }
    else
    {
        Console.WriteLine($"\n A {searcharg} employment found: {foundEmployment}");
    }
}