﻿using E_CommerceApi.Authentication;
using E_CommerceApi.Models.vModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_CommerceApi.Services
{
    public class ShippingProfileService
    {
        private readonly ApplicationDbContext _db;
        public ShippingProfileService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<AllShippingProfiles> GetAll()
        {
            try
            {
                var dbShippingProfiles = await _db.ShippingProfiles.ToListAsync();
                if (dbShippingProfiles.Count > 0)
                {
                    return new AllShippingProfiles
                    {
                        ShippingProfiles = dbShippingProfiles,
                        Message = new ResponseMessage
                        {
                            Message = "Success",
                            Code = 200
                        }
                    };
                }
                else
                {
                    return new AllShippingProfiles
                    {
                        Message = new ResponseMessage
                        {
                            Message = "Empty",
                            Code = 410
                        }
                    };
                }

            }
            catch (Exception ex)
            {
                return new AllShippingProfiles
                {
                    Message = new ResponseMessage
                    {
                        Message = ex.Message,
                        Code = 500
                    }
                };
            }
        }
        public async Task<Models.vModels.ShippingProfileRes> Get(int Id)
        {
            try
            {
                var dbShippingProfile = await _db.ShippingProfiles.SingleOrDefaultAsync(shippingProfile =>  shippingProfile.Id == Id);
                if (dbShippingProfile != null)
                {
                    return new Models.vModels.ShippingProfileRes
                    {
                        ShippingProfileResponse = dbShippingProfile,
                        Message = new ResponseMessage
                        {
                            Message = "Success",
                            Code = 200
                        }
                    };
                }
                else
                {
                    return new Models.vModels.ShippingProfileRes
                    {
                        Message = new ResponseMessage
                        {
                            Message = "NotFound",
                            Code = 404
                        }
                    };
                }

            }
            catch (Exception ex)
            {
                return new Models.vModels.ShippingProfileRes
                {
                    Message = new ResponseMessage
                    {
                        Message = ex.Message,
                        Code = 500
                    }
                };
            }
        }
        public async Task<BaseResponse> Add(Models.dbModels.ShippingProfile ShippingProfile)
        {
            try
            {
                if (_db.ShippingProfiles.Any(ShippingProfile => ShippingProfile.ProfileName == ShippingProfile.ProfileName))
                {
                    return new BaseResponse
                    {
                        Message = new ResponseMessage
                        {
                            Message = "Exist",
                            Code = 510
                        }
                    };
                }
                else
                {
                    await _db.ShippingProfiles.AddAsync(ShippingProfile);
                    await _db.SaveChangesAsync();
                    return new BaseResponse
                    {
                        Message = new ResponseMessage
                        {
                            Message = "Success",
                            Code = 200
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Message = new ResponseMessage
                    {
                        Message = ex.Message,
                        Code = 500
                    }
                };
            }
        }
        public async Task<BaseResponse> Edit(Models.dbModels.ShippingProfile ShippingProfile)
        {
            try
            {
                var dbShippingProfile = await _db.ShippingProfiles.SingleOrDefaultAsync(ShippingProfile => ShippingProfile.Id == ShippingProfile.Id);
                if (dbShippingProfile == null)
                {
                    return new BaseResponse
                    {
                        Message = new ResponseMessage
                        {
                            Message = "NotFound",
                            Code = 404
                        }
                    };
                }
                else
                {
                    _db.Entry(dbShippingProfile).State = EntityState.Modified;
                    await _db.SaveChangesAsync();
                    return new BaseResponse
                    {
                        Message = new ResponseMessage
                        {
                            Message = "Success",
                            Code = 200
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Message = new ResponseMessage
                    {
                        Message = ex.Message,
                        Code = 500
                    }
                };
            }
        }
        public async Task<BaseResponse> Delete(int Id)
        {
            try
            {
                var dbShippingProfile = await _db.ShippingProfiles.SingleOrDefaultAsync(ShippingProfile => ShippingProfile.Id == Id);
                if (dbShippingProfile == null)
                {
                    return new BaseResponse
                    {
                        Message = new ResponseMessage
                        {
                            Message = "NotFound",
                            Code = 404
                        }
                    };
                }
                else
                {
                    _db.ShippingProfiles.Remove(dbShippingProfile);
                    await _db.SaveChangesAsync();
                    return new BaseResponse
                    {
                        Message = new ResponseMessage
                        {
                            Message = "Success",
                            Code = 200
                        }
                    };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    Message = new ResponseMessage
                    {
                        Message = ex.Message,
                        Code = 500
                    }
                };
            }
        }
    }
}
