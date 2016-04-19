using System;
using System.Data.Entity;

namespace jcTM.WebAPI.DataLayer.Entities {
    public partial class jctmEntities {
        public override int SaveChanges() {
            foreach (var item in ChangeTracker.Entries()) {
                switch (item.State) {
                    case EntityState.Deleted:
                        item.Property("Active").CurrentValue = false;
                        item.State = EntityState.Modified;
                        break;
                    case EntityState.Unchanged:
                        continue;
                    case EntityState.Added:
                        item.Property("Created").CurrentValue = DateTime.Now;
                        item.Property("Active").CurrentValue = true;
                        break;
                }

                item.Property("Modified").CurrentValue = DateTime.Now;
            }

            return base.SaveChanges();
        }
    }
}