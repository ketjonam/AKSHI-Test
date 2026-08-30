namespace AKSHI.Test.Core;

[Category("Qytetar")]
[Category("Individual")]
[NonParallelizable]
public abstract class QytetarTestBase : AkshiTestBase
{
    protected override LoginProfile Profile => LoginProfile.Qytetar;
}

[Category("J35413056V")]
[Category("NidJ55728107R")]
public abstract class QytetarNidJ557TestBase : QytetarTestBase
{
}

[Category("J70903019W")]
[Category("NidJ25730113W")]
public abstract class QytetarNidJ257TestBase : QytetarTestBase
{
    protected override LoginProfile Profile => LoginProfile.QytetarJ70903019W;
}

[Category("F60416142P")]
[Category("NidF60214024S")]
[Category("NidG35511058E")]
public abstract class QytetarNidF602TestBase : QytetarTestBase
{
    protected override LoginProfile Profile => LoginProfile.QytetarF60416142P;
}

[Category("Biznes")]
[Category("Organisation")]
[NonParallelizable]
public abstract class BiznesTestBase : AkshiTestBase
{
    protected override LoginProfile Profile => LoginProfile.Biznes;
}
