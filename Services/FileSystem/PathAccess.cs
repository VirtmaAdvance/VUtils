using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using VUtils.Services.Extensions.Strings;

namespace VUtils.Services.FileSystem
{
	public class PathAccess
	{

		private WindowsIdentity _currentUser;
		private WindowsPrincipal _currentPrincipal;
		private SecurityIdentifier? _user => _currentUser.User;



		public PathAccess()
		{
			_currentUser=WindowsIdentity.GetCurrent();
			_currentPrincipal=new (_currentUser);
		}

		public bool HasAccess(string path) => HasAccess(path, FileSystemRights.Read);

		public bool HasAccess(string path, FileSystemRights rights)
		{
			return path.IsFile() ? HasAccess(new DirectoryInfo(path), rights) : HasAccess(new FileInfo(path), rights);
		}

		public bool HasAccess(DirectoryInfo value, FileSystemRights rights) => HasAccess(rights, value.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier)));

		public bool HasAccess(FileInfo value, FileSystemRights rights) => HasAccess(rights, value.GetAccessControl().GetAccessRules(true, true, typeof(SecurityIdentifier)));


		public bool HasAccess(FileSystemRights rights, AuthorizationRuleCollection acl)
		{
			bool allow=false;
			bool inheritedAllow=false;
			bool inheritedDeny=false;
			for(int i = 0;i<acl.Count;i++)
			{
				var currentRule=(FileSystemAccessRule)acl[i]!;
				if(currentRule is not null)
				{
					if((_user is not null) && (_user.Equals(currentRule.IdentityReference) || _currentPrincipal.IsInRole((SecurityIdentifier)currentRule.IdentityReference)))
					{
						if(RuleAccess(currentRule, rights))
						{
							if(currentRule.IsInherited)
								inheritedDeny=true;
							else
								return false;
						}
					}
					else if(currentRule.AccessControlType.Equals(AccessControlType.Allow))
					{
						if(RuleAccess(currentRule, rights))
						{
							if(currentRule.IsInherited)
								inheritedAllow=true;
							else
								allow=true;
						}
					}
				}
			}
			return allow || inheritedAllow && !inheritedDeny;
		}

		private static bool RuleAccess(FileSystemAccessRule currentRule, FileSystemRights rights) => (currentRule.AccessControlType.Equals(rights)) && (currentRule.FileSystemRights & rights) == rights;

	}
}
