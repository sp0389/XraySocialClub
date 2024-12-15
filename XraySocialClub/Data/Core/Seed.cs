using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace XraySocialClub.Data.Core
{
    public class Seed
    {
        public Seed(ModelBuilder mb)
        {
            InitSeedOrganisation(mb);
            InitSeedRoles(mb);
            InitSeedMembers(mb);
            InitSeedMemberRoles(mb);
        }

        private static void InitSeedOrganisation(ModelBuilder mb)
        {
            mb.Entity<Organisation>().HasData(
                new Organisation
                {
                    Id = 1,
                    Name = "XraySocialClub"
                }
            );
        }

        private static void InitSeedRoles(ModelBuilder mb)
        {
            mb.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                    ConcurrencyStamp = "fb519d60-4b07-469b-a11c-b7638a33b636",
                },

                new IdentityRole
                {
                    Id = "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                    Name = "Social",
                    NormalizedName = "SOCIAL",
                    ConcurrencyStamp = "71b21ea0-140f-40e9-b355-a3d015053eb9",
                },

                new IdentityRole
                {
                    Id = "de1e5fe5-585b-4867-aae8-57776d64f330",
                    Name = "Lotto",
                    NormalizedName = "LOTTO",
                    ConcurrencyStamp = "47e5d7e3-4c5c-4e94-aaa2-7bf8a0f1505a",
                },

                new IdentityRole
                {
                    Id = "497fcc8d-a4b1-4fc9-a0d8-9ae50af3cb54",
                    Name = "Pending",
                    NormalizedName = "PENDING",
                    ConcurrencyStamp = "a06787ff-9d57-49de-8d44-5262eec5ad3b"
                }
            );
        }

        private static void InitSeedMembers(ModelBuilder mb)
        {
            mb.Entity<Member>().HasData(
                new Member
                {
                    Id = "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                    UserName = "a.admin@xraysocials.com.au",
                    FirstName = "Alice",
                    LastName = "Admin",
                    OrganisationId = 1,
                    NormalizedUserName = "A.ADMIN@XRAYSOCIALS.COM.AU",
                    Email = "a.admin@xraysocials.com.au",
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEHsSevUsbVfCvzTrAPeOAJGAdLJXoClxNuG4OJyPozgYXexeGOqLXgnIxAZgTQTbfA==",
                    SecurityStamp = "M67EBX32EPBJDLSU75U3EA5SFKIR7MDP",
                    ConcurrencyStamp = "3e098325-ba04-4578-8bd8-231bbf8dde66",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                },
                new Member
                {
                    Id = "c6e5a515-b561-458a-85e6-ab9e7eed58f4",
                    UserName = "l.larry@xraysocials.com.au",
                    FirstName = "Larry",
                    LastName = "Lotto",
                    OrganisationId = 1,
                    NormalizedUserName = "L.LOTTO@XRAYSOCIALS.COM.AU",
                    Email = "l.lotto@xraysocials.com.au",
                    Role = Role.Lotto,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAEGuoaNhuyNZDd/SdkB7dMyKO61l9hBzj4h26Bm6gmQpnrpwe+vNFNyBLSPj0JGM13Q==",
                    SecurityStamp = "ISWZYSPA6TIRY35DE4KKKESEPQZKL6VG",
                    ConcurrencyStamp = "36bea754-e167-42af-83ed-bd78392859f3",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                },
                new Member
                {
                    Id = "7610170e-d0e7-43b9-a289-02d13056d54e",
                    UserName = "s.social@xraysocials.com.au",
                    FirstName = "Sarah",
                    LastName = "Social",
                    OrganisationId = 1,
                    NormalizedUserName = "SARAH.SOCIAL@XRAYSOCIALS.COM.AU",
                    Email = "s.social@xraysocials.com.au",
                    Role = Role.Social,
                    EmailConfirmed = true,
                    PasswordHash = "AQAAAAIAAYagAAAAENaAF8X3fgawsa7CT8EKV1Bm+PGcrq9PhRBL+ee6Rb8lCZVRf/6it+zEesnSHS6q1w==",
                    SecurityStamp = "LZOWMFVS2SAJIT7PFI3CPG4WQDCHQS5R",
                    ConcurrencyStamp = "b2686cbb-099f-4c58-91a4-8fcb9c048d35",
                    PhoneNumber = null,
                    PhoneNumberConfirmed = false,
                    TwoFactorEnabled = false,
                    LockoutEnabled = false,
                    AccessFailedCount = 0,
                }
            );
        }

        private static void InitSeedMemberRoles(ModelBuilder mb)
        {
            mb.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "09adf476-7af7-4bd7-89e5-d173778b3ec9",
                    UserId = "ca32e0e5-46b8-4f44-9a97-0d685a2c54b2",
                },

                new IdentityUserRole<string>
                {
                    RoleId = "1455a748-82ad-4e31-bb41-7c72cfc0fbfa",
                    UserId = "7610170e-d0e7-43b9-a289-02d13056d54e"
                },

                new IdentityUserRole<string>
                {
                    RoleId = "de1e5fe5-585b-4867-aae8-57776d64f330",
                    UserId = "c6e5a515-b561-458a-85e6-ab9e7eed58f4"
                }
            );
        }
    }
}