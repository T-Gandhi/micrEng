using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CarsEngage
{
    public partial class SearchCars : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                // ListBox2.Items.Add("Make");
                var tste = typeof(cars_engage_2022).GetProperties();

                var fieldValues = typeof(cars_engage_2022).GetProperties()
                            .Select(field => field.Name)
                            .ToList();


                foreach (string fieldname in fieldValues)
                {
                    ListBox1.Items.Add(fieldname);

                }

                ListBox2.Items.Add("AND");
                ListBox2.Items.Add("OR");

                Session["filterCondition"] = null;
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
           

            //foreach (ListItem lst in ListBox2.Items)
            //{
            //    if (lst.Selected == true)
            //    {
                   
            //    }

            //}
        }

        public static Expression<Func<cars_engage_2022, bool>> CreatePredicate(string columnName, object searchValue)
        {
            var xType = typeof(cars_engage_2022);
            var x = Expression.Parameter(xType, "x");
            var column = xType.GetProperties().FirstOrDefault(p => p.Name == columnName);

            var body = column == null
                ? (Expression)Expression.Constant(true)
                : Expression.Equal(
                    Expression.PropertyOrField(x, columnName),
                    Expression.Constant(searchValue));

            return Expression.Lambda<Func<cars_engage_2022, bool>>(body, x);
        }

        public void Search(object sender, EventArgs e)
       {


            //Expression.Parameter(Type.GetType("CarsEngage.cars_engage_2022"), "cars_engage_2022");
            //var predicate = PredicateBuilder.True<cars_engage_2022>();



            //if (!string.IsNullOrEmpty(wherecondition.Text))
            //{

            //   var predicate1 = predicate.And(i => i.Make.ToLower().StartsWith(wherecondition.Text) || i.Make.ToLower().StartsWith(wherecondition.Text));

            //    predicate = predicate.And(i =>ListBox1.SelectedItem.Value.StartsWith(wherecondition.Text) || ListBox1.SelectedItem.Value.StartsWith(wherecondition.Text));

            //   //predicate.Body
            //}

            //if (!string.IsNullOrEmpty(Gender))
            //{
            //    int gender;
            //    Int32.TryParse(Gender, out gender);
            //    predicate = predicate.And(i => i.Gender == gender);
            //}
            //if (!string.IsNullOrEmpty(PatientType))
            //{
            //    int type;
            //    Int32.TryParse(PatientType, out type);
            //    predicate = predicate.And(i => i.PatientType == type);
            //}

            //if (!string.IsNullOrEmpty(BirthDate))
            //{
            //    DateTime dob;
            //    DateTime.TryParse(BirthDate, out dob);
            //    predicate = predicate.And(i => EntityFunctions.TruncateTime(i.BirthDate) == EntityFunctions.TruncateTime(dob));
            //}

            //IQueryable<cars_engage_2022> query = ctx.cars_engage_2022.


            //var predicate1 = PredicateBuilder.True<cars_engage_2022>();

            //predicate.And(CreatePredicate("Variant", "Xt"));

            // query = query.Where(predicate);

            if (Session["filterCondition"] != null)
            {

                string[] filterlines = Session["filterCondition"].ToString().Split(
                                new string[] { Environment.NewLine },
                                StringSplitOptions.None
                            );

                List<cars_engage_2022> filterresults = new List<cars_engage_2022>();



                foreach (string item in filterlines)
                {

                    string[] lines = item.Split(';');

                    // predicate = predicate.And(i => ListBox1.SelectedItem.Value.StartsWith(wherecondition.Text) || ListBox1.SelectedItem.Value.StartsWith(wherecondition.Text));


                    var parameterExpression =
        Expression.Parameter(Type.GetType(
            "CarsEngage.cars_engage_2022"), "cars_engage_2022");
                    var constant = Expression.Constant(lines[1].ToString());
                    var property = Expression.Property(parameterExpression,
                        lines[0].ToString());

                    var expression = Expression.Equal(property, constant);

                    //Expression left = Expression.Call(property, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                    //Expression right = Expression.Call(constant, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));


                    //MemberExpression member = Expression.Property(param, filter.PropertyName);
                    //ConstantExpression constant = Expression.Constant(filter.Value);

                    //Expression left = Expression.Call(property, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                    //Expression right = Expression.Call(constant, typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                    //var expression = Expression.Equal(left, right);


                    //var toLower = Expression.Call(property,
                    //             typeof(string).GetMethod("ToLower", System.Type.EmptyTypes));
                    //var condition = Expression.Call(toLower,
                    //                            typeof(string).GetMethod("Contains"),
                    //                            Expression.Constant(lines[1].ToString()));
                    //var expression = Expression.Equal(property, condition);




                    if (lines[2] == "AND")
                    {
                        var constant1 = Expression.Constant(lines[4].ToString());
                        var property1 = Expression.Property(parameterExpression,
                            lines[3].ToString());

                        var expression1 = Expression.Equal(property1, constant1);
                        expression = Expression.And(expression, expression1);
                    }
                    if (lines[2] == "OR")
                    {
                        var constant1 = Expression.Constant(lines[4].ToString());
                        var property1 = Expression.Property(parameterExpression,
                            lines[3].ToString());

                        var expression1 = Expression.Equal(property1, constant1);
                        expression = Expression.Or(expression, expression1);
                    }

                    //------------------------

                    if (lines.Count() >= 7 && lines[5] == "AND")
                    {
                        var constant2 = Expression.Constant(lines[7].ToString());
                        var property2 = Expression.Property(parameterExpression,
                            lines[6].ToString());

                        var expression2 = Expression.Equal(property2, constant2);
                        expression = Expression.And(expression, expression2);
                    }
                    if (lines.Count() >= 7 && lines[5] == "OR")
                    {
                        var constant2 = Expression.Constant(lines[7].ToString());
                        var property2 = Expression.Property(parameterExpression,
                            lines[6].ToString());

                        var expression2 = Expression.Equal(property2, constant2);
                        expression = Expression.Or(expression, expression2);
                    }
                    // -----------

                    if (lines.Count() >= 10 && lines[8] == "AND")
                    {
                        var constant3 = Expression.Constant(lines[10].ToString());
                        var property3 = Expression.Property(parameterExpression,
                            lines[9].ToString());

                        var expression3 = Expression.Equal(property3, constant3);
                        expression = Expression.And(expression, expression3);
                    }
                    if (lines.Count() >= 10 && lines[8] == "OR")
                    {
                        var constant3 = Expression.Constant(lines[10].ToString());
                        var property3 = Expression.Property(parameterExpression,
                            lines[9].ToString());

                        var expression3 = Expression.Equal(property3, constant3);
                        expression = Expression.Or(expression, expression3);
                    }
                    //--------------------------------------
                    if (lines.Count() >= 13 && lines[11] == "AND")
                    {
                        var constant4 = Expression.Constant(lines[13].ToString());
                        var property4 = Expression.Property(parameterExpression,
                            lines[12].ToString());

                        var expression4 = Expression.Equal(property4, constant4);
                        expression = Expression.And(expression, expression4);
                    }
                    if (lines.Count() >= 13 && lines[11] == "OR")
                    {
                        var constant4 = Expression.Constant(lines[13].ToString());
                        var property4 = Expression.Property(parameterExpression,
                            lines[12].ToString());

                        var expression4 = Expression.Equal(property4, constant4);
                        expression = Expression.Or(expression, expression4);
                    }

                    //------------------------

                    if (lines.Count() >= 16 && lines[14] == "AND")
                    {
                        var constant5 = Expression.Constant(lines[16].ToString());
                        var property5 = Expression.Property(parameterExpression,
                            lines[15].ToString());

                        var expression5 = Expression.Equal(property5, constant5);
                        expression = Expression.And(expression, expression5);
                    }
                    if (lines.Count() >= 16 && lines[14] == "OR")
                    {
                        var constant5 = Expression.Constant(lines[16].ToString());
                        var property5 = Expression.Property(parameterExpression,
                            lines[15].ToString());

                        var expression5 = Expression.Equal(property5, constant5);
                        expression = Expression.Or(expression, expression5);
                    }
                    //---------------------------




                    var lambda = Expression.Lambda<Func<cars_engage_2022, bool>>(expression, parameterExpression);

                    var compiledLambda = lambda.Compile();

                    //var predicate = CreatePredicate(lines[0].ToString(), lines[1].ToString());
                    using (var ctx = new CarEngageEntities1())
                    {
                        //var cars_Engage = (from s in ctx.cars_engage_2022
                        //                   where s.Make == "Tata"
                        //                   select s).ToList();


                        var cars_Engage = ctx.cars_engage_2022.Where(compiledLambda).ToList();

                        filterresults.AddRange(cars_Engage);
                    }


                }




                searchResults.DataSource = filterresults;
                searchResults.DataBind();
                resultsPnl.Visible = true;
                resultsPnl.Visible = true;
                resultsPnl.Visible = true;


                //using (var ctx = new CarEngageEntities1())
                //{
                //    //var cars_Engage = (from s in ctx.cars_engage_2022
                //    //                   where s.Make == "Tata"
                //    //                   select s).ToList();
                //    var cars_Engage = ctx.cars_engage_2022.Where(predicate).ToList();


                //    searchResults.DataSource = cars_Engage;
                //    searchResults.DataBind();
                //    resultsPnl.Visible = true;
                //    resultsPnl.Visible = true;
                //    resultsPnl.Visible = true;
                //}
            }

        }
        public void ADD(object sender, EventArgs e)
        {
            // if(Session["filterCondition"]!=null)
            //{
            //    Session["filterCondition"] = Session["filterCondition"]+ Environment.NewLine;
            //}
            if (ListBox1.SelectedItem != null)
            {
                //lblselectedCondition.InnerText = (Session["filterCondition"]==null ?"": Session["filterCondition"] + ";")+ ListBox1.SelectedItem.Text +";" + wherecondition.Text + ";" + (ListBox2.SelectedItem==null ?"": ListBox2.SelectedItem.Text + ";");
                lblselectedCondition.InnerText = (ListBox2.SelectedItem == null ? "" : ListBox2.SelectedItem.Text + ";") + ListBox1.SelectedItem.Text + ";" + wherecondition.Text + ";";
                //lblselectedCondition.InnerText += Environment.NewLine;
                Session["filterCondition"] = (Session["filterCondition"] == null ? "" : Session["filterCondition"]) + lblselectedCondition.InnerText;

                lblselectedCondition.InnerText = (Session["filterCondition"] == null ? "" : Session["filterCondition"].ToString());
            }
        }

    }



    public static class PredicateBuilder
    {
        /// <summary>    
        /// Creates a predicate that evaluates to true.    
        /// </summary>    
        public static Expression<Func<T, bool>> True<T>() { return param => true; }

        /// <summary>    
        /// Creates a predicate that evaluates to false.    
        /// </summary>    
        public static Expression<Func<T, bool>> False<T>() { return param => false; }

        /// <summary>    
        /// Creates a predicate expression from the specified lambda expression.    
        /// </summary>    
        public static Expression<Func<T, bool>> Create<T>(Expression<Func<T, bool>> predicate) { return predicate; }

        /// <summary>    
        /// Combines the first predicate with the second using the logical "and".    
        /// </summary>    
        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.AndAlso);
        }

        /// <summary>    
        /// Combines the first predicate with the second using the logical "or".    
        /// </summary>    
        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.Compose(second, Expression.OrElse);
        }

        /// <summary>    
        /// Negates the predicate.    
        /// </summary>    
        public static Expression<Func<T, bool>> Not<T>(this Expression<Func<T, bool>> expression)
        {
            var negated = Expression.Not(expression.Body);
            return Expression.Lambda<Func<T, bool>>(negated, expression.Parameters);
        }

        /// <summary>    
        /// Combines the first expression with the second using the specified merge function.    
        /// </summary>    
        static Expression<T> Compose<T>(this Expression<T> first, Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            // zip parameters (map from parameters of second to parameters of first)    
            var map = first.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            // replace parameters in the second lambda expression with the parameters in the first    
            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            // create a merged lambda expression with parameters from the first expression    
            return Expression.Lambda<T>(merge(first.Body, secondBody), first.Parameters);
        }

        class ParameterRebinder : ExpressionVisitor
        {
            readonly Dictionary<ParameterExpression, ParameterExpression> map;

            ParameterRebinder(Dictionary<ParameterExpression, ParameterExpression> map)
            {
                this.map = map ?? new Dictionary<ParameterExpression, ParameterExpression>();
            }

            public static Expression ReplaceParameters(Dictionary<ParameterExpression, ParameterExpression> map, Expression exp)
            {
                return new ParameterRebinder(map).Visit(exp);
            }

            protected override Expression VisitParameter(ParameterExpression p)
            {
                ParameterExpression replacement;

                if (map.TryGetValue(p, out replacement))
                {
                    p = replacement;
                }

                return base.VisitParameter(p);
            }
        }
    }
}