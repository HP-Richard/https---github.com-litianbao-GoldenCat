using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using JMReports.Business;

namespace JMReports.WebApp
{
    public partial class LeftTree : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["JMPrincipal"] != null)
            {

                setPermission();
            }

            //FillTree();
        }



        private void setPermission()
        {
            MyPrincipal user = (MyPrincipal)(Session["JMPrincipal"]);
        }

        private void FillTree()
        {
            string strTree = string.Format(@"
                <div class='left'>
  	                <div class='words2'>
    	                <div id='hitButton1'>营销类报表 <em class='iconDown'> </em></div>
	                </div>
                  <div class='group1'>
           	            <div class='line2'>
                            <div>
                                <a href='DailyReport/frmDailyReport.aspx' target='right' onclick='ReportMap(15)' >日报表</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div>
           	            <div class='line2'>
                            <div>
                                <a href='Special/SpecialReport.aspx' target='right' onclick='ReportMap(1)'>酒店专用表</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div> 
           	            <div class='line2'>
                            <div>
                                <a href='Operation/OperationReport.aspx' target='right' onclick='ReportMap(2)'>经营情况一览</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div> 
           	            <div class='line2'>
                            <div>
                                <a href='HotelWhole/HotelWholeReport.aspx' target='right' onclick='ReportMap(3)'>酒店整体</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div>              
    	                <div class='line2'>
                            <div>
                                <a href='RoomMarket/RoomMarketReport2.aspx' target='right' onclick='ReportMap(4)'>客房市场细分</a>
                                <em class='iconRight'></em>
                             </div>
                        </div>
   		                <div class='line2'>
                            <div>
                                <a href='RoomSales/RoomSalesReport.aspx' target='right' onclick='ReportMap(6)'>客房销售渠道</a>
                                <em class='iconRight'></em>
                             </div>
                        </div>
   		                <div class='line2'>
                            <div>
                                <a href='BanquetMarket/BanquetMarketReport.aspx' target='right' onclick='ReportMap(12)'>宴会细分市场</a>
                                <em class='iconRight'></em>
                             </div>
                        </div>  
   		                <div class='line2'>
                            <div>
                                <a href='Unallocate/UnallocateReport.aspx' target='right'>不可分摊成本费用</a>
                                <em class='iconRight'></em>
                             </div>
                        </div> 
   		                <div class='line2'>
        	                <div>
                                <a href='RoomCompete/RoomCompete.aspx' target='right' onclick='ReportMap(7)'>客房竞争组合</a>
                                <em class='iconRight'></em>
                            </div>
                        </div> 
   		                <div class='line2'>
        	                <div>
                                <a href='Restaurant/frmRestaurantVar.aspx' target='right'>各餐厅</a>
                                <em class='iconRight'></em>
                            </div>
                        </div> 
   		                <div class='line2'>
    	         	        <div>
                                <a href='Restaurant/frmRestaurantMain.aspx' target='right'>主要餐厅汇总</a>
                                <em class='iconRight'></em>
                            </div>
                        </div>                                                                                
                  </div>
                    <div class='words2'>
       	                <div id='hitButton2'>效率类报表 <em class='iconDown'> </em> </div>
                    </div>
                    <div class='group2'>
   		                <div class='line2'>
                            <div>
                                <a href='Forecast/ForecastRpt.aspx' target='right' onclick='ReportMap(8)'>预测报表</a>
                                <em class='iconRight'></em>
                            </div>
                        </div>   
   		                <div class='line2'>
                            <div>
                                <a href='Restaurant/frmRestaurantEfficiency.aspx' target='right' onclick='ReportMap(13)'>餐饮部效率表</a>
                                <em class='iconRight'></em>
                            </div>
                        </div>   
   		                <div class='line2'>
                            <div>
                                <a href='Restaurant/frmRestaurantEfficiency.aspx' target='right'>客房部效率表</a>
                                <em class='iconRight'></em>
                            </div>
                        </div>
                    </div>
                    <div class='words2'> 
   	                  <div id='hitButton3'>系统设置<em class='iconDown'> </em></div>
                    </div>
                    <div class='group3'>
           	            <div class='line2'>
                            <div>
                                <a href='User/UserManager.aspx' target='right' onclick='ReportMap(100)'>用户管理</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div>
                        <div class='line2'>
                            <div>
                                <a href='Role/RoleReport.aspx' target='right' onclick='ReportMap(101)'>角色管理</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div>
                        <div class='line2'>
                            <div>
                                <a href='Import/DataImport.aspx' target='right' onclick='ReportMap(102)'>数据导入</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div>
                        <div class='line2'>
                            <div>
                                <a href='Import/DataImportBudget.aspx' target='right' onclick='ReportMap(103)'>预算导入</a>
                                <em class='iconRight'></em>
                             </div>
           	            </div>
                    </div>
                </div>
        ");

            //this.Literal1.Text = strTree;
        }
    }
}