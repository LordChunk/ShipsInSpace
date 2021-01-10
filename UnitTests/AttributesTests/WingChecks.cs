using System.Collections.Generic;
using NUnit.Framework;
using ShipsInSpace.Models;

namespace UnitTests.AttributesTests
{
    public class WingChecks
    {
        public ShipViewModel Ship;
        public ShipsInSpace.Models.CustomAttributes.WingChecks MyWingChecks;

        [SetUp]
        public void Setup()
        {
            Ship = new ShipViewModel
            {
                Id = 1,
                Name = "DeadlyFighterOfDeadliness",
                Wings = new List<WingViewModel>()
                {
                    new WingViewModel()
                    {
                        Id = 4,
                        Name = "O-Fence",
                        Agility = 0,
                        Speed = 0,
                        Energy = 15,
                        NumberOfHardpoints = 2,
                        Weight = 250,
                        Hardpoint = new List<WeaponViewModel>()
                        {
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                        }
                    },
                    new WingViewModel()
                    {
                        Id = 4,
                        Name = "O-Fence",
                        Agility = 0,
                        Speed = 0,
                        Energy = 15,
                        NumberOfHardpoints = 2,
                        Weight = 250,
                        Hardpoint = new List<WeaponViewModel>()
                        {
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                            new WeaponViewModel() {
                                Id = 2,
                                Name= "Crusher",
                                Weight=89,
                                DamageType= DamageTypeEnum.Gravity,
                                EnergyDrain = 56
                            },
                        }
                    },
                },
                NumberOfWings = 2,
                Engine = new EngineViewModel()
                {
                    Id = 3,
                    Name = "Constellation Class",
                    Energy = 200,
                    Weight = 200
                },
                Hull = new HullViewModel
                {
                    Id = 2,
                    Name = "Neptunus",
                    Agility = 100,
                    Speed = 100,
                    ColdShielding = 50,
                    HeatShielding = 0,
                    DefaultMaximumTakeOffMass = TakeOffMassEnum.Tank,
                },
            };

            MyWingChecks = new ShipsInSpace.Models.CustomAttributes.WingChecks();
        }

        [Test]
        public void ShipShouldHaveNoConflict()
        {
            Assert.IsTrue(MyWingChecks.IsValid(Ship.Wings));
        }

        [Test]
        public void CantEquipMoreWeaponsThanSlots()
        {
            Ship.Wings[0].Hardpoint.Add(new WeaponViewModel()
            {
                Id = 14,
                Name = "Nullifier",
                Weight = 23,
                DamageType = DamageTypeEnum.Gravity,
                EnergyDrain = 43
            });

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyWingChecks.IsValid(Ship.Wings));
        }

        [Test]
        public void CantHaveADifferenceInKineticEnergy()
        {
            Ship.Wings[0].Hardpoint[0] = new WeaponViewModel()
            {
                Id = 6,
                Name = "Gauss Gun",
                Weight = 110,
                DamageType = DamageTypeEnum.Kinetic,
                EnergyDrain = 52
            };

            //The isvalid method should return false since this should not be possible and therefore we invert the bool
            Assert.IsTrue(!MyWingChecks.IsValid(Ship.Wings));
        }
    }
}
