using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UI
{
    public static class Constants
    {

        public const string RULES_XML = @"<RuleList>
                                                <Offender>
                                                    <FirstName>Contains A</FirstName>
                                                    <LastName>Contains 1</LastName>
                                                    <DateOfBirth>GreaterThan 13/10/2017</DateOfBirth>
                                                </Offender>  
                                            </RuleList>";

        public const string CLASS_DATA_XML = @"<ClassList>
                                             <Address>
                                              <State>System.String</State>
                                              <City>System.String</City>
                                              <Street>System.String</Street>
                                              <HomeNumber>System.Int32</HomeNumber>
                                              <ZipCode>System.Int32</ZipCode>
                                             </Address>
                                             <Offender>  
                                              <FirstName>System.String</FirstName>  
                                              <LastName>System.String</LastName>  
                                              <DateOfBirth>System.DateTime</DateOfBirth>  
                                             </Offender>
                                            </ClassList>";

        public const String PATH_DIRECTORY = "Repository";

    }
}
