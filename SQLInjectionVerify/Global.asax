<%@ Application Language="C#" %>

<script runat="server" >

   
    void Application_BeginRequest(object sender, EventArgs e) 
    {
         /* 去掉校验功能
        // 在应用程序启动时运行的代码
        //遍历Post参数
      
        foreach (string i in this.Request.Form)
        {
            //对避免URL过长而使用隐藏域（type="hidden"）提交的参数除外，隐藏域的字段名字="__VIEWSTATE"
            if (i == "__VIEWSTATE") continue;
            //非隐藏域的字段，检查是否包含SQL黑名单的特征值
            if (IsIllegals(this.Request.Form[i].ToString()))
            {
                //提示非法SQL字符
                Response.Write("存在非法字符，可能引发SQL注入--Form");
                Response.End();
            }

        }

        //遍历Get参数

        foreach (string j in this.Request.QueryString)
        {
            //非隐藏域的字段，检查是否包含SQL黑名单的特征值
            if (IsIllegals(this.Request.QueryString[j].ToString()))
            {
                //提示非法SQL字符
                Response.Write("存在非法字符，可能引发SQL注入");
                //记录攻击者的IP，非法字符串，时间，页面URL
                Response.End();
            }

        }
         
        //全站防止页面缓存 
        Response.Buffer = true;
        Response.ExpiresAbsolute = DateTime.Now.AddSeconds(-1);
        Response.Expires = 0;
        Response.CacheControl = "no-cache";
          *///去掉校验功能
    }
    //定义校验方法
    public static bool IsIllegals(string InText)
    {
        //需要逐渐细化及精确化这个关键字列表
        string sqlkeyword = "select|insert|update|delete|drop|alter|truncate|exists|union|and|or|xor|order|group|having|exec|execute|openrowset|substring|ascii|xp_cmdshell|join|declare|char|sp_oacreate|wscript.shell|xp_regwrite|'|;|--|*";
        if (InText == null)
            return false;
        foreach (string i in sqlkeyword.Split('|'))
        {
            if ((InText.ToLower().IndexOf(i + " ") > -1) || (InText.ToLower().IndexOf(" " + i) > -1))
            {
                return true;
            }
        }
        return false;
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  在应用程序关闭时运行的代码

    }
        
    void Application_Error(object sender, EventArgs e) 
    { 
        // 在出现未处理的错误时运行的代码

    }

    void Session_Start(object sender, EventArgs e) 
    {
        // 在新会话启动时运行的代码

    }

    void Session_End(object sender, EventArgs e) 
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer
        // 或 SQLServer，则不引发该事件。

    }

       
</script>
