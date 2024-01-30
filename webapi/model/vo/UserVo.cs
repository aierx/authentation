namespace webapi.model.vo;

public class UserVo
{
    public string Name { get; set; }
    
    public string Password { get; set; }

    public List<string> RoleVos { get; set; }
}