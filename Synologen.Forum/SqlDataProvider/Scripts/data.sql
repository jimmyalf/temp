/* Create forum groups */

set identity_insert dbo.tblForumForumGroups on
INSERT INTO dbo.tblForumForumGroups
  (SiteID, ForumGroupID, Name, NewsgroupName, SortOrder)
  VALUES (0, 1, N'Administrators / Moderators', N'', 9999)
INSERT INTO dbo.tblForumForumGroups
  (SiteID, ForumGroupID, Name, NewsgroupName, SortOrder)
  VALUES (0, 2, N'Synologen', N'', 0)
set identity_insert dbo.tblForumForumGroups off

go

/* Create forums */

set identity_insert dbo.tblForumForums on
INSERT INTO dbo.tblForumForums
  (ForumID, SiteID, IsActive, ParentID, ForumGroupID, Name, NewsgroupName, Description, DateCreated, Url, IsModerated, DaysToView, SortOrder, TotalPosts, TotalThreads, DisplayMask, EnablePostStatistics, EnableAutoDelete, EnableAnonymousPosting, AutoDeleteThreshold, MostRecentPostID, MostRecentThreadID, MostRecentThreadReplies, MostRecentPostSubject, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostDate, PostsToModerate, ForumType, IsSearchable)
  VALUES (1, 0, 1, 0, 1, N'Administrators', N'', N'Private forum for global and site administrators', '2005-06-27 14:43:59.803', N'', 0, 7, 1, 0, 0, 0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0, 0, 0, 90, 0, 0, 0, N'', N'', 0, '1977-01-01 00:00:00.000', 0, 0, 1)
INSERT INTO dbo.tblForumForums
  (ForumID, SiteID, IsActive, ParentID, ForumGroupID, Name, NewsgroupName, Description, DateCreated, Url, IsModerated, DaysToView, SortOrder, TotalPosts, TotalThreads, DisplayMask, EnablePostStatistics, EnableAutoDelete, EnableAnonymousPosting, AutoDeleteThreshold, MostRecentPostID, MostRecentThreadID, MostRecentThreadReplies, MostRecentPostSubject, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostDate, PostsToModerate, ForumType, IsSearchable)
  VALUES (2, 0, 1, 0, 1, N'Moderators', N'', N'Private forum for global and site moderators', '2005-06-27 14:43:59.803', N'', 0, 7, 2, 0, 0, 0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0, 0, 0, 90, 0, 0, 0, N'', N'', 0, '1977-01-01 00:00:00.000', 0, 0, 1)
INSERT INTO dbo.tblForumForums
  (ForumID, SiteID, IsActive, ParentID, ForumGroupID, Name, NewsgroupName, Description, DateCreated, Url, IsModerated, DaysToView, SortOrder, TotalPosts, TotalThreads, DisplayMask, EnablePostStatistics, EnableAutoDelete, EnableAnonymousPosting, AutoDeleteThreshold, MostRecentPostID, MostRecentThreadID, MostRecentThreadReplies, MostRecentPostSubject, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostDate, PostsToModerate, ForumType, IsSearchable)
  VALUES (3, 0, 1, 0, 1, N'Reporting Forums', N'', N'When problems are reported they go in this forum.', '2005-06-27 14:43:59.820', N'', 0, 7, 3, 0, 0, 0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0, 0, 1, 90, 0, 0, 0, N'', N'', 0, '1977-01-01 00:00:00.000', 0, 0, 1)
INSERT INTO dbo.tblForumForums
  (ForumID, SiteID, IsActive, ParentID, ForumGroupID, Name, NewsgroupName, Description, DateCreated, Url, IsModerated, DaysToView, SortOrder, TotalPosts, TotalThreads, DisplayMask, EnablePostStatistics, EnableAutoDelete, EnableAnonymousPosting, AutoDeleteThreshold, MostRecentPostID, MostRecentThreadID, MostRecentThreadReplies, MostRecentPostSubject, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostDate, PostsToModerate, ForumType, IsSearchable)
  VALUES (4, 0, 1, 2, 1, N'Deleted Posts', N'', N'Deleted posts are archived in this forum.', '2005-06-27 14:43:59.820', N'', 0, 7, 4, 0, 0, 0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 0, 0, 0, 90, 0, 0, 0, N'', N'', 0, '1977-01-01 00:00:00.000', 0, 0, 1)
INSERT INTO dbo.tblForumForums
  (ForumID, SiteID, IsActive, ParentID, ForumGroupID, Name, NewsgroupName, Description, DateCreated, Url, IsModerated, DaysToView, SortOrder, TotalPosts, TotalThreads, DisplayMask, EnablePostStatistics, EnableAutoDelete, EnableAnonymousPosting, AutoDeleteThreshold, MostRecentPostID, MostRecentThreadID, MostRecentThreadReplies, MostRecentPostSubject, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostDate, PostsToModerate, ForumType, IsSearchable)
  VALUES (10, 0, 1, 0, 2, N'B�gar', N'', N'Prat om b�gar', '2005-06-27 14:43:59.880', N'', 0, 0, 1, 4, 2, 0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 1, 1, 1, 90, 4, 1, 1, N'Re: Welcome Message', N'david', 3, '2005-06-28 10:05:13.320', 0, 0, 1)
INSERT INTO dbo.tblForumForums
  (ForumID, SiteID, IsActive, ParentID, ForumGroupID, Name, NewsgroupName, Description, DateCreated, Url, IsModerated, DaysToView, SortOrder, TotalPosts, TotalThreads, DisplayMask, EnablePostStatistics, EnableAutoDelete, EnableAnonymousPosting, AutoDeleteThreshold, MostRecentPostID, MostRecentThreadID, MostRecentThreadReplies, MostRecentPostSubject, MostRecentPostAuthor, MostRecentPostAuthorID, MostRecentPostDate, PostsToModerate, ForumType, IsSearchable)
  VALUES (11, 0, 1, 0, 2, N'�vrigt', N'', N'Allt mellan himmel och jord', '2007-04-12 17:26:27.997', N'', 0, 7, 0, 3, 2, 0x0000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000, 1, 0, 0, 90, 7, 4, 0, N'asdasdsda', N'nic', 4, '2007-05-07 16:18:20.463', 0, 0, 1)
set identity_insert dbo.tblForumForums off

go


/* Create roles */

set identity_insert dbo.tblForumRoles on
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (0, N'Everyone', N'Do not add users to this role. This role exists only for permission mapping. All anonymous and registered users are automatically part of this role.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (1, N'Global Administrators', N'Most privileged role in the forums. Can administer all sites.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (2, N'Site Administrators', N'Delegate administration for this specific site.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (3, N'Global Moderators', N'Can moderate forums and users in all sites.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (4, N'Site Moderators', N'Can moderate forums and users of this specific site.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (5, N'Global Editors', N'Can approve/reject/edit posts for moderated forums of all sites.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (6, N'Site Editors', N'Can approve/reject/edit posts for moderated forums for this specific site.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (7, N'Global Registered Users', N'All registered users for all sites are automatically added to this role.')
INSERT INTO dbo.tblForumRoles
  (RoleID, Name, Description)
  VALUES (8, N'Site Registered Users', N'This sites registered users are automatically added to this role.')
set identity_insert dbo.tblForumRoles off

go

/* Create user */

set identity_insert dbo.tblForumUsers on
INSERT INTO dbo.tblForumUsers
  (UserID, UserName, Password, PasswordFormat, Salt, PasswordQuestion, PasswordAnswer, Email, DateCreated, LastLogin, LastActivity, LastAction, UserAccountStatus, IsAnonymous, ForceLogin, AppUserToken)
  VALUES (0, N'', N'', 0, N'', N'', N'', N'', '2005-06-27 14:43:59.397', '2005-06-27 14:43:59.397', '2007-05-08 08:40:17.690', N'/:-1:-1:-1:-1:-1', 1, 1, 0, NULL)
INSERT INTO dbo.tblForumUsers
  (UserID, UserName, Password, PasswordFormat, Salt, PasswordQuestion, PasswordAnswer, Email, DateCreated, LastLogin, LastActivity, LastAction, UserAccountStatus, IsAnonymous, ForceLogin, AppUserToken)
  VALUES (2, N'nic', N'pabnic', 0, N'wcMICIz3TTU=', N'', N'', N'niklas@spinit.se', '2007-04-12 17:28:41.970', '2007-05-07 16:02:02.467', '2007-05-07 16:18:28.713', N'post_Active:-1:-1:-1:-1:-1', 1, 0, 0, N'')
set identity_insert dbo.tblForumUsers off

go


/* Create user profiles */

INSERT INTO dbo.tblForumUserProfile
  VALUES (0, 0, 0, 0, 0x00000000, 0x01, 0, 0, 0, 0, 0, 0, 0, 0, 0)
INSERT INTO dbo.tblForumUserProfile
  VALUES (2, 0, 2, 0, 0x0001000000FFFFFFFF01000000000000000C020000004C53797374656D2C2056657273696F6E3D312E302E353030302E302C2043756C747572653D6E65757472616C2C205075626C69634B6579546F6B656E3D6237376135633536313933346530383905010000003253797374656D2E436F6C6C656374696F6E732E5370656369616C697A65642E4E616D6556616C7565436F6C6C656374696F6E0600000008526561644F6E6C790C4861736850726F766964657208436F6D706172657205436F756E74044B6579730656616C756573000303000605013253797374656D2E436F6C6C656374696F6E732E43617365496E73656E73697469766548617368436F646550726F76696465722A53797374656D2E436F6C6C656374696F6E732E43617365496E73656E736974697665436F6D706172657208020000000009030000000904000000000000000905000000090600000004030000003253797374656D2E436F6C6C656374696F6E732E43617365496E73656E73697469766548617368436F646550726F766964657201000000066D5F74657874031D53797374656D2E476C6F62616C697A6174696F6E2E54657874496E666F090700000004040000002A53797374656D2E436F6C6C656374696F6E732E43617365496E73656E736974697665436F6D7061726572010000000D6D5F636F6D70617265496E666F032053797374656D2E476C6F62616C697A6174696F6E2E436F6D70617265496E666F090800000011050000000000000010060000000000000004070000001D53797374656D2E476C6F62616C697A6174696F6E2E54657874496E666F030000000B6D5F6E446174614974656D116D5F757365557365724F766572726964650D6D5F77696E33324C616E674944000000080108CA000000007F00000004080000002053797374656D2E476C6F62616C697A6174696F6E2E436F6D70617265496E666F020000000977696E33324C4349440763756C74757265000008087F0000007F0000000B, 0x01, 1, 0, 0, 0, 0, 1, 0, 0, 0)

go




/* Create forum permissions */

INSERT INTO dbo.tblForumForumPermissions
  VALUES (1, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (1, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 5, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (2, 6, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 0, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 3, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 4, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 5, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 6, 1, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 7, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (3, 8, 2, 2, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 0, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 3, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 4, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 5, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (4, 6, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (10, 0, 1, 1, 1, 1, 1, 1, 0, 0, 0, 0, 0, 0)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (10, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1)
INSERT INTO dbo.tblForumForumPermissions
  VALUES (11, 11, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0)

go

