using MES.Acquintance;
using MES.Data;
using MES.Logic;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MES.Tests {
    [TestFixture]
    public class BatchQueueTest {
        private readonly BatchQueue batches;
        private ILogic logic;
        private IData data;
        private readonly Recipe recipe = new Recipe(1, 1, "1", 1, 1, 1, 1, 1);
        public BatchQueueTest() {
            // must do the following before instantiating batchqueue, we need to
            // access the data layer in order to obtain the current batch id
            logic = new LogicFacade();
            data = new DataFacade();
            logic.InjectData(data);
            batches = new BatchQueue(logic);
        }
        [Test]
        public void MoveUpTest() {
            batches.Batches.Clear();
            SimpleBatch b1 = new SimpleBatch(1,1,1,recipe);
            SimpleBatch b2 = new SimpleBatch(2, 2, 2, recipe);
            batches.Batches.Add(b1);
            batches.Batches.Add(b2);
            batches.MoveUp(b2);
            Assert.AreEqual(b2, batches.Batches[0]);
        }
        [Test]
        public void MoveDownTest() {
            batches.Batches.Clear();
            SimpleBatch b1 = new SimpleBatch(1, 1, 1, recipe);
            SimpleBatch b2 = new SimpleBatch(2, 2, 2, recipe);
            batches.Batches.Add(b1);
            batches.Batches.Add(b2);
            batches.MoveDown(b1);
            Assert.AreEqual(b1, batches.Batches[1]);
        }
        [Test]
        public void CreateBatchTest() {
            batches.Batches.Clear();
            batches.CreateBatch(1, 1, recipe);
            Assert.IsTrue(batches.Batches[0].DesiredAmount == 1 && batches.Batches[0].MachineSpeed == 1);
        }

    }
}
