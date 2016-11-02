import clr
clr.AddReference("System.Core")
import System
clr.ImportExtensions(System.Linq)
from System.Collections.Generic import List

def HW():
    return "Hello World";

def returnList(mylist):
    list1 = List[](["1","2" ,"3"])
    return list1.Union(list).ToList()
